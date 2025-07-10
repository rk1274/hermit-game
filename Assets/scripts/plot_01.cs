using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class plot_01 : MonoBehaviour
{
    public HouseDatabase houseDB;

    public SpriteRenderer artworkSprite;

    public PlayerInventory playerInventory;

    private int selectedOption = 0;

    private bool empty;

    private void Start()
    {
        if (playerInventory.HouseCount == 1)
        {
            empty = true;

        }
        else
        {
            if (!PlayerPrefs.HasKey(gameObject.name))
            {
                selectedOption = 0;
            }
            else
            {
                Load();
            }
            UpdateHouse(selectedOption);
        }

    }

    private void UpdateHouse(int selectedOption)
    {
        House house = playerInventory.GetHouse(selectedOption);
        artworkSprite.sprite = house.Sprite;
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt(gameObject.name);
    }
}
