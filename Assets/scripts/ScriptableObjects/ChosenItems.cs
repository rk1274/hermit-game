using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ChosenItems : ScriptableObject
{
    public Item[] items = new Item[5];

    public Item potentialCrab;

    public int ItemCount
    {
        get { return items.Length; }
    }

    public Item[] GetItems()
    {
        return items;
    }

    public void RemoveAllItems()
    {
        items = new Item[5];
    }

    public void RemoveItem(int index)
    {
        items[index] = null;
    }

    public void SetItem(Item item, int index)
    {
        items[index] = item;
        Debug.Log("ITEM:"+index);
    }

    public void SetCrab(Item crab)
    {
        potentialCrab = crab;
    }

    public Item GetCrab()
    {
        return potentialCrab;
    }

    public void ClearCrab()
    {
        potentialCrab = null;
    }
}

