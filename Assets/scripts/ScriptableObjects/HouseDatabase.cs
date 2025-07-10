using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HouseDatabase : ScriptableObject
{
    [SerializeField] private House[] houses;

    public int HouseCount => houses.Length;

    public House GetHouse(int index)
    {
        if (index < 0 || index >= HouseCount)
        {
            Debug.LogWarning($"GetHouse: Index {index} is out of bounds.");

            return null;
        }

        return houses[index];
    }

    public House GetHouseByName(string name)
    {
        foreach (House house in houses)
        {
            if (house != null && house.Name == name)
                return house;
        }

        Debug.LogWarning($"GetHouseByName: No house found with name '{name}'.");
        return null;
    }

    public House GetNextHouse(House currentHouse)
    {
        for (int i = 0; i < HouseCount - 1; i++)
        {
            if (houses[i] != null && houses[i].Name == currentHouse.Name)
            {
                return houses[i + 1];
            }
        }

        Debug.LogWarning($"GetNextHouse: '{currentHouse?.Name}' is the last house or not found.");
        return null;
    }
}
