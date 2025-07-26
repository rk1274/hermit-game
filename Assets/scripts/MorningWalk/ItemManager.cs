using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
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

    [Header("UI")]
    [SerializeField] private TMP_Text itemCountDisplay;

    [Header("UI Popups")]
    [SerializeField] private GameObject crabWarningPopupPrefab;
    [SerializeField] private GameObject noSpacePopupPrefab;
    [SerializeField] private Canvas worldCanvas;

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

        itemCountDisplay.text = $"{itemCount} / {inventory.MaxPickupCount}";
    }

    public void SetChosen(GameObject itemLocationObj)
    {
        Button itemLocation = itemLocationObj.GetComponent<Button>();
        StartCoroutine(HandleItemSelection(itemLocation, itemLocationObj));
    }

    private IEnumerator HandleItemSelection(Button itemLocation, GameObject itemLocationObj)
    {
        if (itemCount >= inventory.MaxPickupCount)
        {
            ShowNoSpacePopup(itemLocationObj.transform.position);

            yield break;
        }

        Item item = locationItemMap[itemLocation];
        if (item.Type == Item.ItemType.Crab)
        {
            if (inventory.Capacity <= internalCrabCount)
            {
                ShowCrabPopup(itemLocationObj.transform.position);

                yield break;
            }

            yield return StartCoroutine(HandleCrabSelection(item, itemLocationObj));
        }
        else
        {
            itemLocationObj.SetActive(false);
            AddNewSelection(item);
        }

        itemCountDisplay.text = $"{itemCount} / {inventory.MaxPickupCount}";
    }

    private void ShowNoSpacePopup(Vector3 itemWorldPosition)
    {
        Vector3 popupOffset = new Vector3(0, 0.5f, 0);
        Vector3 popupPosition = itemWorldPosition + popupOffset;

        GameObject popup = Instantiate(noSpacePopupPrefab, worldCanvas.transform);

        popup.transform.position = popupPosition;

        TMP_Text text = popup.GetComponentInChildren<TMP_Text>();
        text.text = "No space for more items!";

        CanvasGroup canvasGroup = popup.GetComponent<CanvasGroup>();
        StartCoroutine(FadeAndDestroy(canvasGroup, popup, 1.5f));
    }

    private IEnumerator FadeAndDestroy(CanvasGroup canvasGroup, GameObject popup, float duration)
    {
        float elapsed = 0f;
        float startAlpha = canvasGroup.alpha;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsed / duration);
            yield return null;
        }

        Destroy(popup);
    }

    private void ShowCrabPopup(Vector3 crabWorldPosition)
    {
        Vector3 popupOffset = new Vector3(0, 0.5f, 0);
        Vector3 popupPosition = crabWorldPosition + popupOffset;

        GameObject popup = Instantiate(crabWarningPopupPrefab, worldCanvas.transform);

        popup.transform.position = popupPosition;

        TMP_Text text = popup.GetComponentInChildren<TMP_Text>();
        text.text = "No space for more crabs!";

        CanvasGroup canvasGroup = popup.GetComponent<CanvasGroup>();
        StartCoroutine(FadeAndDestroy(canvasGroup, popup, 1.5f));
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