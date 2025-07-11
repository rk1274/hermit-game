using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoBar_Manager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image houseSprite;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private GameObject upgradeButtonContainer;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private GameObject upgradeInfo;

    [Header("References")]
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private HouseDatabase houseDatabase;
    [SerializeField] private Inventory_Manager inventoryManager;

    private House curHouse;

    public void UpdateInfo(House house)
    {
        curHouse = house;

        houseSprite.sprite = house.Sprite;
        levelText.SetText($"Lv. {house.Level}");
        nameText.SetText(house.Name);
        quantityText.SetText(house.CrabAmount.ToString());

        bool isMaxLevel = house.Level >= house.MaxLevel;

        upgradeButtonContainer.SetActive(!isMaxLevel);
        upgradeInfo.SetActive(!isMaxLevel);

        if (isMaxLevel)
        {
            costText.text = string.Empty;
            return;
        }

        House nextHouse = houseDatabase.GetNextHouse(house);
        costText.SetText($"{nextHouse.ShellCost}          {nextHouse.PearlCost}");
        quantityText.SetText($"{house.CrabAmount} -> {nextHouse.CrabAmount}");

        bool canAffordUpgrade = inventory.Shells >= nextHouse.ShellCost && inventory.Pearls >= nextHouse.PearlCost;
        upgradeButton.interactable = canAffordUpgrade;
    }

    public void UpgradeHouse()
    {
        House newHouse = houseDatabase.GetNextHouse(curHouse);
        inventory.UpdateHouse(curHouse, newHouse);
        UpdateInfo(newHouse);
        inventoryManager.RefreshInv();

        inventory.AddPearls(-newHouse.PearlCost);
        inventory.AddShells(-newHouse.ShellCost);
    }
}
