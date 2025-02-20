using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class House
{
    public Sprite houseSprite;
    public string name;
    public bool inUse;

    public int shellCost;
    public int pearlCost;
    public int crabAmount;

    public int max_level;
    public int level;

    public object Shallowcopy()
    {
        return this.MemberwiseClone();
    }
}
