using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HouseManager : MonoBehaviour
{
    public HouseDatabase houseDB;
    public PlotSO plot;

    public SpriteRenderer artworkSprite;

    public PlayerInventory playerInventory;
    public ItemDatabase itemDB;

    private int selectedOption = 0;

    private bool empty;

    private void Start()
    {
        if(playerInventory.HouseCount == 1)
        {
            empty = true; 

        }else if (CheckInUse())
        {
            empty = true;
        }
        else
        {
            if (!PlayerPrefs.HasKey(plot.CurrentPlot))
            {
                selectedOption = 0;
            }

            UpdateHouse(selectedOption);
        }
    }

    public bool CheckInUse()
    {
        Load();

        for (int i = 1; i < playerInventory.HouseCount; i++)
        {
            if(playerInventory.GetHouse(i).InUse == false)
            {
                return false;
            }
        }
        return true;
    }

    public void NextOption()
    {
        if (empty)
        {
            return;
        }
        selectedOption++;

        if(selectedOption >= playerInventory.HouseCount)
        {
            selectedOption = 0;
        }

        if (playerInventory.GetHouse(selectedOption).InUse)
        {
            NextOption();
            Debug.Log(":p");
        }

        Debug.Log(selectedOption.ToString());
        UpdateHouse(selectedOption);
        Save();
    }

    public void BackOption()
    {
        if (empty)
        {
            return;
        }
        selectedOption--;

        if (selectedOption < 0)
        {
            selectedOption = playerInventory.HouseCount - 1;
        }

        if (playerInventory.GetHouse(selectedOption).InUse)
        {
            BackOption();
            Debug.Log(":p");
        }

        Debug.Log(selectedOption.ToString());
        UpdateHouse(selectedOption);
        Save();
    }

    private void UpdateHouse(int selectedOption)
    {
        House house = playerInventory.GetHouse(selectedOption);
        artworkSprite.sprite = house.Sprite;

    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt(plot.CurrentPlot);

        playerInventory.RemoveCapacity(selectedOption);

        // TODO - make a 'inUse' function for playerinv
        playerInventory.GetHouse(selectedOption).InUse = false;
    }

    private void Save()
    {
        PlayerPrefs.SetInt(plot.CurrentPlot, selectedOption);
    }

    public void ChangeScene(string sceneID)
    {
        if(selectedOption != 0)
        {
            if (playerInventory.GetHouse(selectedOption).Name.Equals("flame_tree_01") || playerInventory.GetHouse(selectedOption).Name.Equals("flame_tree_02"))
            {
                Debug.Log("here");
                itemDB.SetActive("crab_s_fire");
            }
            playerInventory.AddCapacity(selectedOption);
            playerInventory.GetHouse(selectedOption).InUse = true;
        }
        SceneManager.LoadScene(sceneID);
    }
}
