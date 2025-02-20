using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "Examples/PlotSO")]
public class PlotSO : ScriptableObject
{
    private string currPlot;

    public Sprite[] plotSprites;

    public void setPlot(string plotNum)
    {
        currPlot = plotNum;
    }

    public string getPlot()
    {
        return currPlot;
    }
}
