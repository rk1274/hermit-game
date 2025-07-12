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
    private List<Button> itemLocations;

    [Header("Game State")]
    public PlayerInventory inventory;
    public GameObject morningWalk;
    public GameObject newCrabUI;

    [Header("Managers")]
    public NewCrabManager crabManager;
    public MoveCamera cameraController;

    [Header("Generation Settings")]
    public GameObject buttonPrefab;
    public RectTransform buttonParent;
    public int buttonCount = 5;

    private float xMin = -340f;
    private float xMax = -76f;

    private float yMin = -7293f;
    private float yMax = 13156;
    private float yMinDiff = 200f;
    private float yMaxDiff = 1000f;

    private float minZRotation = -30f;
    private float maxZRotation = 30f;

    private Dictionary<Button, Item> locationItemMap;
    private int itemCount = 0;
    private int internalCrabCount;
    public CrabStatus crabStatus = CrabStatus.Inactive;

    private void Start()
    {
        if (itemLocationContainer != null)
        {
            generateItemLocations();
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

        locationItemMap = new Dictionary<Button, Item>(itemLocations.Count);
        internalCrabCount = inventory.CrabCount;
        chosenItems.ClearAllItems();

        foreach (Button itemLocation in itemLocations)
        {
            var item = GetRandomActiveItem();
            itemLocation.GetComponent<Image>().sprite = item.Sprite;

            locationItemMap[itemLocation] = item;
        }
    }

    private void generateItemLocations()
    {
        if (buttonPrefab == null || buttonParent == null)
        {
            Debug.LogError("Missing buttonPrefab or buttonParent reference.");
            return;
        }
    
        locationItemMap = new Dictionary<Button, Item>(buttonCount);
        itemLocations = new List<Button>();
        
        float currentY = yMin;
        int locationNum = 1;

        while (currentY < yMax)
        {
            GameObject itemLocationObj = createNewLocation(locationNum);

            setLocationPosition(itemLocationObj, currentY);

            getAndSetItem(itemLocationObj);

            locationNum++;
            currentY += Random.Range(yMinDiff, yMaxDiff);
        }
    }

    private GameObject createNewLocation(int num)
    {
        GameObject newLocation = Instantiate(buttonPrefab, buttonParent);
        newLocation.name = $"location_{num}";

        return newLocation;
    }

    private void setLocationPosition(GameObject itemLocationObj, float currentY)
    {
        RectTransform rt = itemLocationObj.GetComponent<RectTransform>();

        float x = Random.Range(xMin, xMax);
        float zRotation = Random.Range(minZRotation, maxZRotation);

        rt.anchoredPosition = new Vector2(x, currentY);
        rt.localEulerAngles = new Vector3(0f, 0f, zRotation);
    }

    private void getAndSetItem(GameObject itemLocationObj)
    {
        Button btn = itemLocationObj.GetComponent<Button>();
        Item item = GetRandomActiveItem();

        btn.GetComponent<Image>().sprite = item.Sprite;

        itemLocations.Add(btn);
        locationItemMap[btn] = item;
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