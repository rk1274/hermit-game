using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Compilation;

public class AfterWalkLoader : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private ChosenItems chosenItems;
    [SerializeField] private CrabDatabase crabDatabase;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text shellsText;
    [SerializeField] private TMP_Text pearlsText;
    [SerializeField] private TMP_Text crabsText;

    [Header("New Day Summary")]
    [SerializeField] private GameObject newDaySummaryPanel;
    [SerializeField] private GameObject walkSummaryContainer;
    [SerializeField] private GameObject crabSummaryContainer;

    private int prevShells;
    private int prevPearls;
    private int prevCrabs;

    private void Start()
    {
        if (chosenItems.Items[0] == null || string.IsNullOrEmpty(chosenItems.Items[0].Name)) 
        {
            Debug.Log("No items chosen.");
            return;
        }

        processChosenItems();

        LoadSummary();

        updateUI();
        chosenItems.ClearAllItems();
    }

    private void processChosenItems()
    {
        prevShells = inventory.Shells;
        prevPearls = inventory.Pearls;
        prevCrabs = inventory.CrabCount;

        foreach (Item item in chosenItems.Items)
        {
            if (item == null || string.IsNullOrEmpty(item.Name))
                continue;

            switch (item.Type)
            {
                case Item.ItemType.Shell:
                    inventory.AddShells(1);
                    break;

                case Item.ItemType.Pearl:
                    inventory.AddPearls(1);
                    break;

                case Item.ItemType.Crab:
                    Crab crab = crabDatabase.GetCrab(item.Name);
                    inventory.AddCrab(crab);
                    break;

                default:
                    Debug.LogWarning($"Unknown item type: {item.Name}");
                    break;
            }
        }
    }

    private void LoadSummary()
    {
        newDaySummaryPanel.SetActive(true);

        walkSummaryContainer.transform.Find("shells").GetComponent<TMP_Text>().text =
            $"{prevShells} >> {inventory.Shells}";

        walkSummaryContainer.transform.Find("pearls").GetComponent<TMP_Text>().text =
            $"{prevPearls} >> {inventory.Pearls}";

        walkSummaryContainer.transform.Find("crabs").GetComponent<TMP_Text>().text =
            $"{prevCrabs} >> {inventory.CrabCount}";

        crabSummaryContainer.transform.Find("shells").GetComponent<TMP_Text>().text =
            $"+ {inventory.Shells - prevShells}";

        crabSummaryContainer.transform.Find("pearls").GetComponent<TMP_Text>().text =
            $"+ {inventory.Pearls - prevPearls}";

        crabSummaryContainer.transform.Find("crabs").GetComponent<TMP_Text>().text =
            $"+ {inventory.CrabCount - prevCrabs}";
    }

    private void updateUI()
    {
        shellsText.text = inventory.Shells.ToString();
        pearlsText.text = inventory.Pearls.ToString();
        crabsText.SetText($"{inventory.CrabCount} / {inventory.Capacity}");
    }
    
    public void CloseSummary()
    {
        newDaySummaryPanel.SetActive(false);
    }
}