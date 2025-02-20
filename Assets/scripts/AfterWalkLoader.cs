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
        Item[] items = chosenItems.GetItems();

        for (int i = 0; i < items.Length; i++)
        {
            if(items[i] != null && !string.IsNullOrEmpty(items[i].name))
            {
                string[] name = items[i].name.Split('_');
                if (name[0] == "shell")
                {
                    Debug.Log("SHELL FOUND!");
                    inventory.addShells(1);
                }
                else if (name[0] == "pearl")
                {
                    Debug.Log("PEARL FOUND!");
                    inventory.addPearls(1);
                }
                else if (name[0] == "crab")
                {
                    Debug.Log("CRAB FOUND!");
                    Crab crab = crabDB.GetCrab(items[i].name);
                    inventory.addCrab(crab);

                }
            }

        }

        shells.text = inventory.shells.ToString();
        pearls.text = inventory.pearls.ToString();
        crabs.SetText(inventory.crabs.Count.ToString()+" / "+inventory.capacity);

        chosenItems.RemoveAllItems();

    }
}
