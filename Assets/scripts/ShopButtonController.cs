using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtonController : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public HouseDatabase houseDB;

    public ItemDatabase itemDB;

    private int neededShell;
    private int neededPearl;
    private int capacity;
    

    public void SelectCoco()
    {
        House curHouse = houseDB.NameGetHouse("coco_01");

        UpdateInventory(-curHouse.shellCost, -curHouse.pearlCost, curHouse);
    }

    public void SelectLog()
    {
        House curHouse = houseDB.NameGetHouse("log_01");

        UpdateInventory(-curHouse.shellCost, -curHouse.pearlCost, curHouse);
    }

    public void SelectGlass()
    {
        House curHouse = houseDB.NameGetHouse("glass_01");

        UpdateInventory(-curHouse.shellCost, -curHouse.pearlCost, curHouse);
    }

    public void SelectBall()
    {
        House curHouse = houseDB.NameGetHouse("beach_ball_01");

        UpdateInventory(-curHouse.shellCost, -curHouse.pearlCost, curHouse);
    }

    public void SelectFlame()
    {
        House curHouse = houseDB.NameGetHouse("flame_tree_01");

        UpdateInventory(-curHouse.shellCost, -curHouse.pearlCost, curHouse);
    }

    public void UpdateInventory(int neededShell, int neededPearl, House house)
    {
        if (playerInventory.shells >= -neededShell && playerInventory.pearls >= -neededPearl)
        {
            playerInventory.addShells(neededShell);
            playerInventory.addPearls(neededPearl);

            playerInventory.addHouse(house);
            Debug.Log("Purchased!!");
        }
        else
        {
            Debug.Log("not enough funds");
        }
    }
}
