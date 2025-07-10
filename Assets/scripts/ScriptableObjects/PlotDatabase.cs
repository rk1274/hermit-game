using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlotDatabase : ScriptableObject
{
    [SerializeField] private Plot[] plots;

    public int PlotCount => plots.Length;
    public Plot GetPlot(int index) => plots[index];
    public House GetPlotHouse(int index) =>  plots[index].house;
}