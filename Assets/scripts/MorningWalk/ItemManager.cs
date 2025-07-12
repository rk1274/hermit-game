using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour
{
    public enum CrabStatus
    {
        Inactive,
        Discarded,
        Kept
    }

    [Header("Databases")]
    public ItemDatabase itemDB;
    public ChosenItems chosenItems;

    [Header("UI")]
    public GameObject itemLocationContainer;
    private Button[] itemLocations;

    [Header("Game State")]
    public PlayerInventory inventory;
    public GameObject morningWalk;
    public GameObject newCrabUI;

    [Header("Managers")]
    public NewCrabManager crabManager;
    public MoveCamera cameraController;

    private Dictionary<Button, Item> locationItemMap;
    private int itemCount = 0;
    private int internalCrabCount;
    public CrabStatus crabStatus = CrabStatus.Inactive;

    private void Start()
    {
        if (itemLocationContainer != null)
        {
            itemLocations = itemLocationContainer.GetComponentsInChildren<Button>(true);
        }
        else
        {
            Debug.LogWarning("Button container is not assigned.");
        }

        if (itemDB.ActiveItemCount == 0)
        {
            Debug.LogWarning("Active items empty at start, calling Reset() to populate.");
            itemDB.Reset();
        }

        locationItemMap = new Dictionary<Button, Item>(itemLocations.Length);
        internalCrabCount = inventory.CrabCount;
        chosenItems.ClearAllItems();

        foreach (Button itemLocation in itemLocations)
        {
            var item = GetRandomActiveItem();
            itemLocation.GetComponent<Image>().sprite = item.Sprite;

            locationItemMap[itemLocation] = item;
        }
    }

    private Item GetRandomActiveItem()
    {
        int index = Random.Range(0, itemDB.ActiveItemCount);
        return itemDB.GetActiveItem(index);
    }

    public void SetChosen(GameObject itemLocationObj)
    {
        Button itemLocation = itemLocationObj.GetComponent<Button>();
        StartCoroutine(HandleItemSelection(itemLocation, itemLocationObj));
    }

    private IEnumerator HandleItemSelection(Button itemLocation, GameObject itemLocationObj)
    {
        if (itemCount >= 5) yield break;

        Item item = locationItemMap[itemLocation];
        string[] nameParts = item.Name.Split('_');

        if (nameParts[0] == "crab")
        {
            if (inventory.Capacity <= internalCrabCount)
            {
                Debug.Log("No space for more crabs.");
                yield break;
            }

            yield return StartCoroutine(HandleCrabSelection(item, itemLocationObj));
        }
        else
        {
            itemLocationObj.SetActive(false);
            AddNewSelection(item);
        }
    }

    private IEnumerator HandleCrabSelection(Item item, GameObject itemLocationObj)
    {
        handleNewCrabUI(item, itemLocationObj);

        UnityEngine.Vector3 originalCameraPos = Camera.main.transform.position;
        int originalSpeed = cameraController.Speed;

        pauseScroll();

        crabManager.SetCrab();

        yield return new WaitUntil(() => crabStatus != CrabStatus.Inactive);

        resumeScroll(originalCameraPos, originalSpeed);

        if (crabStatus == CrabStatus.Kept)
        {
            internalCrabCount++;
            AddNewSelection(item);
        }

        crabStatus = CrabStatus.Inactive;
    }

    private void pauseScroll()
    {
        cameraController.Speed = 0;
        cameraController.transform.position = new UnityEngine.Vector3(0, 0, -10);
    }

     private void resumeScroll(UnityEngine.Vector3 originalCameraPos, int originalSpeed)
    {
        cameraController.Speed = originalSpeed;
        cameraController.transform.position = originalCameraPos;
        morningWalk.SetActive(true);
        newCrabUI.SetActive(false);
    }

    private void handleNewCrabUI(Item item, GameObject itemLocationObj)
    {
        itemLocationObj.SetActive(false);
        chosenItems.SetCrab(item);

        morningWalk.SetActive(false);
        newCrabUI.SetActive(true);
    }

    private void AddNewSelection(Item item)
    {
        chosenItems.SetItem(item, itemCount);
        itemCount++;
    }
}