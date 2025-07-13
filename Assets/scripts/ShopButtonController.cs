using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtonController : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private HouseDatabase houseDB;
    [SerializeField] private ItemDatabase itemDB;

    public void SelectHouse(GameObject houseObj)
    {
        string houseName = houseObj.name;
        House curHouse = houseDB.GetHouseByName(houseName);

        UpdateInventory(-curHouse.ShellCost, -curHouse.PearlCost, curHouse);
    }

    public void UpdateInventory(int neededShell, int neededPearl, House house)
    {
        if (playerInventory.Shells >= -neededShell && playerInventory.Pearls >= -neededPearl)
        {
            playerInventory.AddShells(neededShell);
            playerInventory.AddPearls(neededPearl);

            playerInventory.AddHouse(house);
            Debug.Log("Purchased!!");
        }
        else
        {
            Debug.Log("not enough funds");
        }
    }
}
