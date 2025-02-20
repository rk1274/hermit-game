using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_initializer : MonoBehaviour
{
    public ItemDatabase iDB;
    public PlayerInventory inv;
    public HouseDatabase hDB;
    public PlotDatabase pDB;

    public void NewGame()
    {
        inv.houses.Clear();
        inv.crabs.Clear();
        inv.plots.Clear();
        inv.addHouse(hDB.GetHouse(0));
        inv.addHouse(hDB.GetHouse(1));
        inv.addPlot(pDB.GetPlot(0));
        inv.capacity = 0;
        inv.shells = 0;
        inv.pearls = 0;

        iDB.Reset();

        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetInt("edit_isActive", 0);
        SceneManager.LoadScene("main_beach");
    }

    public void Continue()
    {
        SceneManager.LoadScene("main_beach");
    }
}
