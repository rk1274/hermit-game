using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    private void Start()
    {
        ProcessChosenItems();
        UpdateUI();
        chosenItems.ClearAllItems();
    }

    private void ProcessChosenItems()
    {
        foreach (Item item in chosenItems.Items)
        {
            if (item == null || string.IsNullOrEmpty(item.Name))
                continue;

            string[] nameParts = item.Name.Split('_');
            string category = nameParts[0];

            switch (category)
            {
                case "shell":
                    inventory.AddShells(1);
                    break;

                case "pearl":
                    inventory.AddPearls(1);
                    break;

                case "crab":
                    Crab crab = crabDatabase.GetCrab(item.Name);
                    inventory.AddCrab(crab);
                    break;

                default:
                    Debug.LogWarning($"Unknown item type: {item.Name}");
                    break;
            }
        }
    }

    private void UpdateUI()
    {
        shellsText.text = inventory.Shells.ToString();
        pearlsText.text = inventory.Pearls.ToString();
        crabsText.SetText($"{inventory.CrabCount} / {inventory.Capacity}");
    }
}