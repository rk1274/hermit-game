using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class PlayerInventory : ScriptableObject
{
    [Header("Resources")]
    [SerializeField] private int shells = 1;
    [SerializeField] private int pearls = 1;
    [SerializeField] private int capacity = 0;

    [Header("Owned Objects")]
    [SerializeField] private List<Crab> crabs = new List<Crab>();
    [SerializeField] private List<House> houses = new List<House>();
    [SerializeField] private List<Plot> plots = new List<Plot>();

    public int Shells => shells;
    public int Pearls => pearls;
    public int Capacity => capacity;
    public List<Crab> Crabs => crabs;

    public int HouseCount => houses.Count;
    public int CrabCount => crabs.Count;
    public int PlotCount => plots.Count;

    public void AddCrab(Crab crab) => crabs.Add(crab);
    public void AddPlot(Plot plot) => plots.Add(plot);

    public void AddShells(int amount) => shells += amount;
    public void AddPearls(int amount) => pearls += amount;

    public void AddCapacity(House house) => capacity += house.crabAmount;
    public void RemoveCapacity(House house) => capacity -= house.crabAmount;

    public void AddCapacity(int index) => capacity += GetHouse(index).crabAmount;
    public void RemoveCapacity(int index) => capacity -= GetHouse(index).crabAmount;

    public void AddHouse(House house)
    {
        houses.Add((House)house.Shallowcopy());
        AddCapacity(house);
    }

    public void UpdateHouse(House current, House updated)
    {
        int index = houses.IndexOf(current);

        if (index == -1)
        {
            Debug.LogWarning("House to update not found in inventory.");
            return;
        }

        bool wasPreviouslyInUse = houses[index].inUse;

        if (wasPreviouslyInUse)
        {
            RemoveCapacity(houses[index]);
            AddCapacity(updated);
        }

        houses[index] = (House)updated.Shallowcopy();
        houses[index].inUse = wasPreviouslyInUse;
    }

    public House GetHouse(int index)
    {
        if (index < 0 || index >= houses.Count)
        {
            Debug.LogWarning($"Invalid house index: {index} / Count: {HouseCount}");
            return null;
        }
        return houses[index];
    }

    public void ResetInventory()
    {
        crabs.Clear();
        plots.Clear();
        houses.Clear();
        capacity = 0;
        shells = 0;
        pearls = 0;
    }
}
