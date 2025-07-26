using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CrabInfoManager : MonoBehaviour
{
    [Header("Resources")]
    [SerializeField] private PlayerInventory inventory;

    [Header("Containers")]
    [SerializeField] private GameObject shell;
    [SerializeField] private GameObject body;

    private void Start()
    {
        int crabID = PlayerPrefs.GetInt("selected_crab");
        Crab crab = inventory.GetCrab(crabID);

        updateCrab(crab);
    }

    private void updateCrab(Crab crab)
    {
        setSprite(body, crab.Body.Sprite);
        setSprite(shell, crab.Shell.Sprite);
    }

    public void setSprite(GameObject go, Sprite sprite)
    {
        SpriteRenderer renderer = go.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.sprite = sprite;
        }
        else
        {
            Debug.LogWarning($"{go.name} is missing a SpriteRenderer.");
        }
    }

    public void Return()
    {
        SceneManager.LoadScene("main_beach");
    }
}