using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Manager : MonoBehaviour
{
    public PlayerInventory inventory;

    public GameObject infoBar;
    public InfoBar_Manager infoBarManager;

    public HouseDatabase houseDatabase;

    public Image[] images;

    private void Start()
    {
        if(inventory.HouseCount == 1)
        {
            AddHouses();
        }
        
        for (int i = 0; i < inventory.HouseCount - 1; i++)
        {
            images[i].transform.parent.gameObject.SetActive(true);
            images[i].sprite = inventory.GetHouse(i + 1).Sprite;
        }
    }

    public void UpdateInfoBar(int index)
    {
        infoBar.SetActive(true);
        House curHouse = inventory.GetHouse(index);

        infoBarManager.UpdateInfo(curHouse);
    }

    public void RefreshInv()
    {
        for (int i = 0; i < inventory.HouseCount - 1; i++)
        {
            images[i].transform.parent.gameObject.SetActive(true);
            images[i].sprite = inventory.GetHouse(i + 1).Sprite;
        }
    }

    // used for testing
    public void AddHouses()
    {
        inventory.AddHouse(houseDatabase.GetHouse(1));
        inventory.AddHouse(houseDatabase.GetHouse(2));
        inventory.AddHouse(houseDatabase.GetHouse(3));
        inventory.AddHouse(houseDatabase.GetHouse(4));
        inventory.AddHouse(houseDatabase.GetHouse(5));
        inventory.AddHouse(houseDatabase.GetHouse(6));
        inventory.AddHouse(houseDatabase.GetHouse(7));
        inventory.AddHouse(houseDatabase.GetHouse(8));
        inventory.AddHouse(houseDatabase.GetHouse(9));
    }

}
