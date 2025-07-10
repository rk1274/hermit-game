using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Manager : MonoBehaviour
{
    public PlayerInventory inv;

    public GameObject info_bar;
    public InfoBar_Manager info_bm;

    public HouseDatabase house_db;

    public Image[] images;

    private void Start()
    {
        if(inv.HouseCount == 1)
        {
            AddHouses();
        }
        for (int i = 0; i < inv.HouseCount-1; i++)
        {
            images[i].transform.parent.gameObject.SetActive(true);
            images[i].sprite = inv.GetHouse(i+1).houseSprite;
        }
    }

    public void update_info_bar(int index)
    {
        info_bar.SetActive(true);
        House curHouse = inv.GetHouse(index);

        info_bm.Update_Info(curHouse);
    }

    public void RefreshInv()
    {
        for (int i = 0; i < inv.HouseCount - 1; i++)
        {
            images[i].transform.parent.gameObject.SetActive(true);
            images[i].sprite = inv.GetHouse(i + 1).houseSprite;
        }
    }

    // used for testing
    public void AddHouses()
    {
        inv.AddHouse(house_db.GetHouse(1));
        inv.AddHouse(house_db.GetHouse(2));
        inv.AddHouse(house_db.GetHouse(3));
        inv.AddHouse(house_db.GetHouse(4));
        inv.AddHouse(house_db.GetHouse(5));
        inv.AddHouse(house_db.GetHouse(6));
        inv.AddHouse(house_db.GetHouse(7));
        inv.AddHouse(house_db.GetHouse(8));
        inv.AddHouse(house_db.GetHouse(9));
    }

}
