using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class BeachItemGenerator : MonoBehaviour
{
public enum CrabStatus
    {
        Inactive,
        Discarded,
        Kept
    }

    [Header("Databases")]
    public ItemDatabase itemDB;

    [Header("UI")]
    public GameObject itemLocationContainer;
    private List<Button> itemLocations;

    [Header("Generation Settings")]
    public GameObject buttonPrefab;
    public RectTransform buttonParent;

    private float xMin = -340f;
    private float xMax = -76f;

    private float yMin = -7293f;
    private float yMax = 13156;
    private float yMinDiff = 200f;
    private float yMaxDiff = 1000f;

    private float minZRotation = -30f;
    private float maxZRotation = 30f;

    private Dictionary<Button, Item> locationItemMap;

    public Dictionary<Button, Item> GenerateBeachItems()
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

        foreach (Button itemLocation in itemLocations)
        {
            var item = GetRandomActiveItem();
            itemLocation.GetComponent<Image>().sprite = item.Sprite;

            locationItemMap[itemLocation] = item;
        }

        return locationItemMap;
    }

    private void generateItemLocations()
    {
        if (buttonPrefab == null || buttonParent == null)
        {
            Debug.LogError("Missing buttonPrefab or buttonParent reference.");
            return;
        }
    
        locationItemMap = new Dictionary<Button, Item>();
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
}