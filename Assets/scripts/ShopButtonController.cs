using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtonController : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private HouseDatabase houseDB;
    [SerializeField] private ItemDatabase itemDB;

    public void SelectCoco()
    {
        House curHouse = houseDB.GetHouseByName("coco_01");

        UpdateInventory(-curHouse.ShellCost, -curHouse.PearlCost, curHouse);
    }

    public void SelectLog()
    {
        House curHouse = houseDB.GetHouseByName("log_01");

        UpdateInventory(-curHouse.ShellCost, -curHouse.PearlCost, curHouse);
    }

    public void SelectGlass()
    {
        House curHouse = houseDB.GetHouseByName("glass_01");

        UpdateInventory(-curHouse.ShellCost, -curHouse.PearlCost, curHouse);
    }

    public void SelectBall()
    {
        House curHouse = houseDB.GetHouseByName("beach_ball_01");

        UpdateInventory(-curHouse.ShellCost, -curHouse.PearlCost, curHouse);
    }

    public void SelectFlame()
    {
        House curHouse = houseDB.GetHouseByName("flame_tree_01");

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
