using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public enum ItemCategory
    {
        Normal,
        Special
    }

    public enum ItemType
    {
        Shell,
        Pearl,
        Crab,
    }

    public Sprite Sprite;
    public string Name;
    public bool Active;
    public ItemCategory Category;
    public ItemType Type;
}
