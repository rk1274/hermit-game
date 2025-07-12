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
    public ChosenItems chosenItems;

    [Header("Game State")]
    public PlayerInventory inventory;
    public GameObject morningWalk;
    public GameObject newCrabUI;

    [Header("Managers")]
    public NewCrabManager crabManager;
    public MoveCamera cameraController;

    public BeachItemGenerator beachItemGenerator;

    private Dictionary<Button, Item> locationItemMap;
    private int itemCount = 0;
    private int internalCrabCount;
    public CrabStatus crabStatus = CrabStatus.Inactive;

    private void Start()
    {
        locationItemMap = beachItemGenerator.GenerateBeachItems();
        internalCrabCount = inventory.CrabCount;
        chosenItems.ClearAllItems();
    }

    public void SetChosen(GameObject itemLocationObj)
    {
        Button itemLocation = itemLocationObj.GetComponent<Button>();
        StartCoroutine(HandleItemSelection(itemLocation, itemLocationObj));
    }

    private IEnumerator HandleItemSelection(Button itemLocation, GameObject itemLocationObj)
    {
        if (itemCount >= 5)
        {
            Debug.Log("Max items reached.");

            yield break;
        }

        Item item = locationItemMap[itemLocation];
        if (item.Type == Item.ItemType.Crab)
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