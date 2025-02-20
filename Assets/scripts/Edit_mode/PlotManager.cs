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
        for (int i = 0; i < inventory.plots.Count; i++)
        {
            plotButtons[i].SetActive(true);
            plotSprites[i].SetActive(true);
        }
    }


    public void SetShopPanel()
    {
        int index = inventory.plots.Count;

        if(index < PDB.PlotCount)
        {
            newPlot = PDB.GetPlot(index);

            cost_text.SetText(newPlot.shellCost.ToString() + "          " + newPlot.pearlCost.ToString());
        }
        else
        {
            infoBar.SetActive(false);
        }
    }

    public void Buy()
    {
        if(inventory.shells >= newPlot.shellCost && inventory.pearls >= newPlot.pearlCost)
        {
            inventory.addPlot(newPlot);
            inventory.addShells(-newPlot.shellCost);
            inventory.addPearls(-newPlot.pearlCost);

            DisplayPlots();
            SetShopPanel();
        }
    }





    
}
