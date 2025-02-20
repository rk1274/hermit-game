using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemDatabase : ScriptableObject
{
    public Item[] items;
    public List<Item> activeItems;

    public int ItemCount
    {
        get { return items.Length; }
    }

    public int ActiveItemCount
    {
        get { return activeItems.Count; }
    }

    public Item GetItem(int index)
    {
        return items[index];
    }

    public void SetActive(string itemName){
        foreach (Item foundItem in items)
        {
            if (foundItem.name == itemName)
            {
                foundItem.active = true;
                activeItems.Add(foundItem);
            }
        }
    }

    public Item GetActiveItem(int index)
    {
        Debug.Log(index + "..." + ActiveItemCount);
        return activeItems[index];
    }

    public void Reset(){
        Debug.Log("Resetttttinggg");

        activeItems = new List<Item>();

        for (int i = 0; i < ItemCount; i++){
            string[] name = items[i].name.Split('_');

            if (name[1] == "n"){
                items[i].active = true;
                activeItems.Add(items[i]);

                continue;
            }
                
            items[i].active = false;
        }
    }
}
