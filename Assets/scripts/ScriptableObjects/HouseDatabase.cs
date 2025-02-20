using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HouseDatabase : ScriptableObject
{
    public House[] house;


    public int HouseCount
    {
        get { return house.Length; }
    }

    public House GetHouse(int index)
    {
        return house[index];
    }

    public House NameGetHouse(string name)
    {
        foreach (House foundHouse in house)
        {
            if (foundHouse.name == name)
            {
                return foundHouse;
            }
        }
        return null;
    }

    public House GetNextHouse(House curHouse)
    {
        for (int i = 0; i < house.Length; i++)
        {
            if (house[i].name == curHouse.name)
            {
                return house[i+1];
            }
        }
        return null;
    }
}
