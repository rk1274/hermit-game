using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoBar_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public Image house_sprite;
    public TMP_Text level_text;

    public TMP_Text name_text;

    public TMP_Text cost_text;
    public TMP_Text quantity_text;

    public House curHouse;

    public PlayerInventory inv;
    public HouseDatabase HDB;

    public GameObject upgradeBut;
    public Button upgrade;

    public Inventory_Manager inventory;

    public GameObject upgrade_info;

    public void Update_Info(House house)
    {
        if (house.MaxLevel == house.Level)
        {
            upgradeBut.SetActive(false);
            upgrade_info.SetActive(false);
            quantity_text.SetText(house.CrabAmount.ToString());
        }
        else 
        {
            if (inv.Pearls >= curHouse.PearlCost && inv.Shells >= curHouse.ShellCost)
            {
                upgradeBut.SetActive(true);
                upgrade.interactable = true;
            }
            else
            {
                upgradeBut.SetActive(true);
                upgrade.interactable = false;
            }
            upgrade_info.SetActive(true);
            House newHouse = HDB.GetNextHouse(house);
            cost_text.SetText(newHouse.ShellCost.ToString() + "          " + newHouse.PearlCost.ToString());
            quantity_text.SetText(house.CrabAmount.ToString()+" -> "+newHouse.CrabAmount.ToString());

        }
        house_sprite.sprite = house.Sprite;
        level_text.SetText("Lv."+house.Level.ToString());
        name_text.SetText(house.Name);
        
        curHouse = house;
    }

    public void Upgrade_house()
    {
        House newHouse = HDB.GetNextHouse(curHouse);
        inv.UpdateHouse(curHouse, newHouse);
        Update_Info(newHouse);
        inventory.RefreshInv();
        inv.AddPearls(-newHouse.PearlCost);
        inv.AddShells(-newHouse.ShellCost);
    }
}
