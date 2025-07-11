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
    public Button[] buttons;

    [Header("Game State")]
    public PlayerInventory inventory;
    public GameObject morningWalk;
    public GameObject newCrabUI;

    [Header("Managers")]
    public NewCrabManager crabManager;
    public move_camera cameraController;

    private Dictionary<Button, Item> buttonItemMap;
    private int itemCount = 0;
    private int internalCrabCount;
    public CrabStatus crabStatus = CrabStatus.Inactive;

    private void Start()
    {
        if (itemDB.ActiveItemCount == 0)
        {
            Debug.LogWarning("Active items empty at start, calling Reset() to populate.");
            itemDB.Reset();
        }

        buttonItemMap = new Dictionary<Button, Item>(buttons.Length);
        internalCrabCount = inventory.CrabCount;
        chosenItems.ClearAllItems();

        foreach (Button button in buttons)
        {
            var item = GetRandomActiveItem();
            button.GetComponent<Image>().sprite = item.Sprite;
            buttonItemMap[button] = item;
        }
    }

    private Item GetRandomActiveItem()
    {
        int index = Random.Range(0, itemDB.ActiveItemCount);
        return itemDB.GetActiveItem(index);
    }

    public void SetChosen(GameObject buttonObj)
    {
        Button button = buttonObj.GetComponent<Button>();
        StartCoroutine(HandleItemSelection(button, buttonObj));
    }

    private IEnumerator HandleItemSelection(Button button, GameObject buttonObj)
    {
        if (itemCount >= 5) yield break;

        Item item = buttonItemMap[button];
        string[] nameParts = item.Name.Split('_');

        if (nameParts[0] == "crab")
        {
            if (inventory.Capacity <= internalCrabCount)
            {
                Debug.Log("No space for more crabs.");
                yield break;
            }

            yield return StartCoroutine(HandleCrabSelection(button, buttonObj));
        }
        else
        {
            buttonObj.SetActive(false);
            AddNewSelection(button);
        }
    }

    private IEnumerator HandleCrabSelection(Button button, GameObject buttonObj)
    {
        handleNewCrabUI(button, buttonObj);

        UnityEngine.Vector3 originalCameraPos = Camera.main.transform.position;
        int originalSpeed = cameraController.speed;

        pauseScroll();

        crabManager.SetCrab();

        yield return new WaitUntil(() => crabStatus != CrabStatus.Inactive);

        resumeScroll(originalCameraPos, originalSpeed);

        if (crabStatus == CrabStatus.Kept)
        {
            internalCrabCount++;
            AddNewSelection(button);
        }

        crabStatus = CrabStatus.Inactive;
    }

    private void pauseScroll()
    {
        cameraController.speed = 0;
        cameraController.transform.position = new UnityEngine.Vector3(0, 0, -10);
    }

     private void resumeScroll(UnityEngine.Vector3 originalCameraPos, int originalSpeed)
    {
        cameraController.speed = originalSpeed;
        cameraController.transform.position = originalCameraPos;
        morningWalk.SetActive(true);
        newCrabUI.SetActive(false);
    }

    private void handleNewCrabUI(Button button, GameObject buttonObj)
    {
        buttonObj.SetActive(false);
        chosenItems.SetCrab(buttonItemMap[button]);

        morningWalk.SetActive(false);
        newCrabUI.SetActive(true);
    }

    private void AddNewSelection(Button button)
    {
        Item item = buttonItemMap[button];
        chosenItems.SetItem(item, itemCount);
        itemCount++;
    }
}