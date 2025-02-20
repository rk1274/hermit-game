using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class PlayerInventory : ScriptableObject
{
    public int shells = 1;
    public int pearls = 1;
    public int capacity;

    public List<Crab> crabs;

    public List<House> houses;

    public List<Plot> plots;

    public void addCrab(Crab crab)
    {
        crabs.Add(crab);
    }

    public void addHouse(House house)
    {
        houses.Add((House)house.Shallowcopy());
    }

    public void addPlot(Plot plot)
    {
        plots.Add(plot);
    }

    public void UpdateHouse(House curHouse, House newHouse)
    {   
        if (houses[houses.IndexOf(curHouse)].inUse == true)
        {
            removeCapacity(curHouse);
            addCapacity(newHouse);
            int index = houses.IndexOf(curHouse);
            Debug.Log(index);
            houses[houses.IndexOf(curHouse)] = (House)newHouse.Shallowcopy();
            GetHouse(index).inUse = true;
        }
        else
        {
            houses[houses.IndexOf(curHouse)] = (House)newHouse.Shallowcopy();
        }
    }

    public int HouseCount
    {
        get { return houses.Count; }
    }

    public House GetHouse(int index)
    {
        try
        {
            return houses[index];
        }
        catch
        {
            Debug.Log(index + " :p "+HouseCount);
            return null;
        }
    }

    public void addShells(int num)
    {
        shells = shells + num;
    }
    public void addPearls(int num)
    {
        pearls = pearls + num;
    }

    public void addCapacity(int num)
    {
        capacity = capacity + GetHouse(num).crabAmount;
    }

    public void removeCapacity(int num)
    {
        capacity = capacity - GetHouse(num).crabAmount;
    }

    public void addCapacity(House house)
    {
        capacity = capacity + house.crabAmount;
    }

    public void removeCapacity(House house)
    {
        capacity = capacity - house.crabAmount;
    }

}
