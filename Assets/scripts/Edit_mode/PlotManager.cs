using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlotManager : MonoBehaviour
{
    public PlayerInventory inventory;

    public PlotDatabase PDB;

    public GameObject[] plotButtons;
    public GameObject[] plotSprites;

    public TMP_Text cost_text;

    public GameObject infoBar;

    private Plot newPlot;

    void Start()
    {
        DisplayPlots();
        SetShopPanel();
    }

    public void DisplayPlots()
    {
        for (int i = 0; i < inventory.PlotCount; i++)
        {
            plotButtons[i].SetActive(true);
            plotSprites[i].SetActive(true);
        }
    }


    public void SetShopPanel()
    {
        int index = inventory.PlotCount;

        if(index < PDB.PlotCount)
        {
            newPlot = PDB.GetPlot(index);

            cost_text.SetText(newPlot.ShellCost.ToString() + "          " + newPlot.PearlCost.ToString());
        }
        else
        {
            infoBar.SetActive(false);
        }
    }

    public void Buy()
    {
        if(inventory.Shells >= newPlot.ShellCost && inventory.Pearls >= newPlot.PearlCost)
        {
            inventory.AddPlot(newPlot);
            inventory.AddShells(-newPlot.ShellCost);
            inventory.AddPearls(-newPlot.PearlCost);

            DisplayPlots();
            SetShopPanel();
        }
    }





    
}
