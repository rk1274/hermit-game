using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class House
{
    public Sprite Sprite;
    public string Name;
    public bool InUse;

    public int ShellCost;
    public int PearlCost;
    public int CrabAmount;

    public int MaxLevel;
    public int Level;

    public bool IsSpecial;
    public string UnlockedItemName;

    public object ShallowCopy()
    {
        return this.MemberwiseClone();
    }
}
