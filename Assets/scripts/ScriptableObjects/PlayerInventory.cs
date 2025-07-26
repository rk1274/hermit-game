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
    [SerializeField] private int maxPickupCount = 5;

    [Header("Owned Objects")]
    [SerializeField] private List<CrabItem> crabs = new List<CrabItem>();
    [SerializeField] private List<House> houses = new List<House>();
    [SerializeField] private List<Plot> plots = new List<Plot>();

    private int crabID = 1;

    public int Shells => shells;
    public int Pearls => pearls;
    public int Capacity => capacity;
    public int MaxPickupCount => maxPickupCount;
    public List<CrabItem> Crabs => crabs;

    public int HouseCount => houses.Count;
    public int CrabCount => crabs.Count;
    public int PlotCount => plots.Count;

    public void AddCrab(Crab crab) {
        CrabItem crabItem = new CrabItem(crab, crabID);
        crabID++;

        crabs.Add(crabItem);
    }
    public void AddPlot(Plot plot) => plots.Add(plot);

    public void AddShells(int amount) => shells += amount;
    public void AddPearls(int amount) => pearls += amount;

    public void AddCapacity(House house) => capacity += house.CrabAmount;
    public void RemoveCapacity(House house) => capacity -= house.CrabAmount;

    public void AddCapacity(int index) => capacity += GetHouse(index).CrabAmount;
    public void RemoveCapacity(int index) => capacity -= GetHouse(index).CrabAmount;

    public void AddHouse(House house)
    {
        houses.Add((House)house.ShallowCopy());
    }

    public void UpdateHouse(House current, House updated)
    {
        int index = houses.IndexOf(current);

        if (index == -1)
        {
            Debug.LogWarning("House to update not found in inventory.");
            return;
        }

        bool wasPreviouslyInUse = houses[index].InUse;

        if (wasPreviouslyInUse)
        {
            RemoveCapacity(houses[index]);
            AddCapacity(updated);
        }

        houses[index] = (House)updated.ShallowCopy();
        houses[index].InUse = wasPreviouslyInUse;
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

    public CrabItem GetCrab(int crabID)
    {
        foreach (CrabItem crab in crabs)
        {
            if (crab.ID == crabID) {
                return crab;
            }
        }

        return null;
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
