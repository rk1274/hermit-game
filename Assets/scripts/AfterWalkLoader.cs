using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AfterWalkLoader : MonoBehaviour
{
    public PlayerInventory inventory;

    public ChosenItems chosenItems;
    public CrabDatabase crabDB;

    public TMP_Text shells;
    public TMP_Text pearls;
    public TMP_Text crabs;

    private void Start()
    {
        Item[] items = chosenItems.Items;

        for (int i = 0; i < items.Length; i++)
        {
            if(items[i] != null && !string.IsNullOrEmpty(items[i].name))
            {
                string[] name = items[i].name.Split('_');
                if (name[0] == "shell")
                {
                    Debug.Log("SHELL FOUND!");
                    inventory.AddShells(1);
                }
                else if (name[0] == "pearl")
                {
                    Debug.Log("PEARL FOUND!");
                    inventory.AddPearls(1);
                }
                else if (name[0] == "crab")
                {
                    Debug.Log("CRAB FOUND!");
                    Crab crab = crabDB.GetCrab(items[i].name);
                    inventory.AddCrab(crab);
                }
            }

        }

        shells.text = inventory.Shells.ToString();
        pearls.text = inventory.Pearls.ToString();
        crabs.SetText(inventory.CrabCount.ToString()+" / "+inventory.Capacity);

        chosenItems.ClearAllItems();

    }
}
