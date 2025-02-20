using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditButton : MonoBehaviour
{

    public GameObject mainCanvas;
    public GameObject editCanvas;

    public void Start()
    {
        if (PlayerPrefs.GetInt("edit_isActive") == 0)
        {
            mainCanvas.SetActive(true);
            editCanvas.SetActive(false);
        }
        else
        {
            mainCanvas.SetActive(false);
            editCanvas.SetActive(true);
        }
    }
    public void setActive()
    {
        if (PlayerPrefs.GetInt("edit_isActive") == 1)
        {
            mainCanvas.SetActive(true);
            editCanvas.SetActive(false);
            Debug.Log("Active: false");
            PlayerPrefs.SetInt("edit_isActive", 0);
        }
        else
        {
            mainCanvas.SetActive(false);
            editCanvas.SetActive(true);
            Debug.Log("Active: true");
            PlayerPrefs.SetInt("edit_isActive", 1);
        }
    }

}
