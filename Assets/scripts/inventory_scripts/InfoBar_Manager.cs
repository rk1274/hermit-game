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
        if (house.max_level == house.level)
        {
            upgradeBut.SetActive(false);
            upgrade_info.SetActive(false);
            quantity_text.SetText(house.crabAmount.ToString());
        }
        else 
        {
            if (inv.Pearls >= curHouse.pearlCost && inv.Shells >= curHouse.shellCost)
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
            cost_text.SetText(newHouse.shellCost.ToString() + "          " + newHouse.pearlCost.ToString());
            quantity_text.SetText(house.crabAmount.ToString()+" -> "+newHouse.crabAmount.ToString());

        }
        house_sprite.sprite = house.houseSprite;
        level_text.SetText("Lv."+house.level.ToString());
        name_text.SetText(house.name);
        
        curHouse = house;
    }

    public void Upgrade_house()
    {
        House newHouse = HDB.GetNextHouse(curHouse);
        inv.UpdateHouse(curHouse, newHouse);
        Update_Info(newHouse);
        inventory.RefreshInv();
        inv.AddPearls(-newHouse.pearlCost);
        inv.AddShells(-newHouse.shellCost);
    }
}
