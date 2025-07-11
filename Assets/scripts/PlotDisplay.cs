using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlotDisplay : MonoBehaviour
{
    [SerializeField] private SpriteRenderer artworkSprite;
    [SerializeField] private PlayerInventory playerInventory;

    private int selectedOption = 0;

    private void Start()
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
