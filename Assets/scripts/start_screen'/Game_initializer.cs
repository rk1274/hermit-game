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
        inv.ResetInventory();

        inv.AddHouse(hDB.GetHouse(0));
        inv.AddHouse(hDB.GetHouse(1));
        inv.AddPlot(pDB.GetPlot(0));

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
