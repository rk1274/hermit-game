using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ChosenItems : ScriptableObject
{
    private const int MaxItems = 5;

    [SerializeField] private Item[] items = new Item[5];
    [SerializeField] private Item potentialCrab;

    public int ItemCount => items.Length;

    public Item[] Items => items;
    public Item Crab => potentialCrab;

    public void ClearAllItems()
    {
        items = new Item[MaxItems];
    }

    public void RemoveItem(int index)
    {
        if (index >= 0 && index < MaxItems)
        {
            items[index] = null;
        }
        else
        {
            Debug.LogWarning($"RemoveItem: Invalid index {index}");
        }
    }

    public void SetItem(Item item, int index)
    {
        if (index >= 0 && index < MaxItems)
        {
            items[index] = item;
            Debug.Log($"SetItem: Set item at index {index} to {item?.Name ?? "null"}");
        }
        else
        {
            Debug.LogWarning($"SetItem: Invalid index {index}");
        }
    }

    public void SetCrab(Item crab)
    {
        potentialCrab = crab;
    }

    public void ClearCrab()
    {
        potentialCrab = null;
    }
}

