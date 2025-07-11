using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlotManager : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private PlotDatabase plotDatabase;

    [SerializeField] private GameObject[] plotButtons;
    [SerializeField] private GameObject[] plotSprites;

    [SerializeField] private TMP_Text costText;
    [SerializeField] private GameObject infoBar;

    private Plot nextPlot;

    void Start()
    {
        UpdatePlotDisplay();
        UpdateShopPanel();
    }

    public void UpdatePlotDisplay()
    {
        for (int i = 0; i < inventory.PlotCount; i++)
        {
            plotButtons[i].SetActive(true);
            plotSprites[i].SetActive(true);
        }
    }

    public void UpdateShopPanel()
    {
        int index = inventory.PlotCount;

        if(index < plotDatabase.PlotCount)
        {
            nextPlot = plotDatabase.GetPlot(index);
            costText.text = $"{nextPlot.ShellCost}          {nextPlot.PearlCost}";
        }
        else
        {
            nextPlot = null;
            infoBar.SetActive(false);
        }
    }

    public void Buy()
    {
        if (nextPlot == null)
        {
            Debug.LogWarning("No plot available to buy.");
            return;
        }

        if (inventory.Shells >= nextPlot.ShellCost && inventory.Pearls >= nextPlot.PearlCost)
        {
            inventory.AddPlot(nextPlot);
            inventory.AddShells(-nextPlot.ShellCost);
            inventory.AddPearls(-nextPlot.PearlCost);

            UpdatePlotDisplay();
            UpdateShopPanel();
        }
        else
        {
            Debug.Log("Not enough resources to buy plot.");
        }
    }





    
}
