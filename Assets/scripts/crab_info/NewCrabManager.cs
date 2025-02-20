using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewCrabManager : MonoBehaviour
{
    public ChosenItems chosenItems;
    public CrabDatabase crabDB;

    public GameObject body;
    public GameObject shell;

    public ItemManager itemManager;

    public void SetCrab()
    {
        Item crabItem = chosenItems.GetCrab();

        Debug.Log("CRAB FOUND!");
        Crab crab = crabDB.GetCrab(crabItem.name);

        body.GetComponent<SpriteRenderer>().sprite = crab.body.sprite;
        shell.GetComponent<SpriteRenderer>().sprite = crab.shell.sprite;
    }

    public void Discard()
    {
        itemManager.crabStatus = 1;
        chosenItems.ClearCrab();
    }

    public void Keep()
    {
        itemManager.crabStatus = 2;
        chosenItems.ClearCrab();
    }
}
