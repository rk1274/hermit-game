using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonSceneManager : MonoBehaviour
{
    [SerializeField] private PlotSO plotSO;

    public void LoadScene(string sceneName)
    {
        if (PlayerPrefs.GetInt("edit_isActive") != 1)
        {
            Debug.Log("Edit not active, cannot click.");

            return;
        }

        plotSO.CurrentPlot = EventSystem.current.currentSelectedGameObject.name;
        SceneManager.LoadScene(sceneName);
    }

}
