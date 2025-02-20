using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlotDatabase : ScriptableObject
{
    public Plot[] plots;


    public int PlotCount
    {
        get { return plots.Length; }
    }

    public Plot GetPlot(int index)
    {
        return plots[index];
    }

    public House GetPlotHouse(int index)
    {
        return plots[index].house;
    }
    
}
