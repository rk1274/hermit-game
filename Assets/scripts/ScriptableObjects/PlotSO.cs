using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "Examples/PlotSO")]
public class PlotSO : ScriptableObject
{
    [SerializeField] private string currPlot;
    [SerializeField] private Sprite[] plotSprites;

    public Sprite[] PlotSprites => plotSprites;

    public string CurrentPlot
    {
        get => currPlot;
        set => currPlot = value;
    }
}
