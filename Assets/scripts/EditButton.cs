using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditButton : MonoBehaviour
{
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject editCanvas;

    private void Start()
    {
        if (PlayerPrefs.GetInt("edit_isActive") == 0)
        {
            mainCanvas.SetActive(true);
            editCanvas.SetActive(false);

            return;
        }

        mainCanvas.SetActive(false);
        editCanvas.SetActive(true);
    }

    public void setActive()
    {
        if (PlayerPrefs.GetInt("edit_isActive") == 1)
        {
            mainCanvas.SetActive(true);
            editCanvas.SetActive(false);

            PlayerPrefs.SetInt("edit_isActive", 0);

            return;
        }

        mainCanvas.SetActive(false);
        editCanvas.SetActive(true);
        PlayerPrefs.SetInt("edit_isActive", 1);
    }
}
