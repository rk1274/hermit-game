using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemDatabase : ScriptableObject
{
    [SerializeField] private Item[] items;
    [SerializeField] private List<Item> activeItems;

    public int ItemCount => items.Length;
    public int ActiveItemCount => activeItems.Count;

    public Item GetItem(int index)
    {
        if (index < 0 || index >= ItemCount)
        {
            Debug.LogWarning($"Item index {index} is out of range.");
            return null;
        }

        return items[index];
    }

    public void SetActive(string itemName) 
    {
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
        if (index < 0 || index >= ActiveItemCount) 
        {
            Debug.LogWarning($"Active item index {index} is out of range.");

            return null;
        }

        return activeItems[index];
    }

    public void Reset()
    {
        activeItems.Clear();

        foreach (Item item in items)
        {
            string[] nameParts = item.name.Split('_');
            if (nameParts.Length > 1 && nameParts[1] != "n")
            {
                item.active = false;

                continue;
            }

            item.active = true;
            activeItems.Add(item);
        }
    }
}
