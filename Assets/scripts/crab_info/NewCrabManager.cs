using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewCrabManager : MonoBehaviour
{
    [SerializeField] private ChosenItems chosenItems;
    [SerializeField] private CrabDatabase crabDB;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject shell;
    [SerializeField] private ItemManager itemManager;

    public void SetCrab()
    {
        Item crabItem = chosenItems.Crab;
        Crab crab = crabDB.GetCrab(crabItem.Name);

        body.GetComponent<SpriteRenderer>().sprite = crab.Body.Sprite;
        shell.GetComponent<SpriteRenderer>().sprite = crab.Shell.Sprite;
    }

    public void Discard()
    {
        itemManager.crabStatus = ItemManager.CrabStatus.Discarded;
        chosenItems.ClearCrab();
    }

    public void Keep()
    {
        itemManager.crabStatus =  ItemManager.CrabStatus.Kept;
        chosenItems.ClearCrab();
    }
}
