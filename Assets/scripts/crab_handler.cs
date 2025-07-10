using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crab_handler : MonoBehaviour
{
    public GameObject[] crabGOs = new GameObject[5];
    public PlayerInventory inventory;
    public List<Crab> crabs;
    private Transform body;
    private Transform shell;
    private SpriteRenderer spriteR;
    
    void Start()
    {
        crabs = inventory.Crabs;
        if(crabs.Count > 0)
        {
            for (int i = 0; i < crabs.Count; i++)
            {
                Crab crab = crabs[i];
                UpdateBodySprite(crabGOs[i].transform.Find("body").gameObject, crab);
                UpdateShellSprite(crabGOs[i].transform.Find("shell").gameObject, crab);
                crabGOs[i].SetActive(true);
            }
        }
    }

    private void UpdateBodySprite(GameObject go, Crab crab)
    {
        spriteR = go.GetComponent<SpriteRenderer>();
        spriteR.sprite = crab.body.sprite;
    }

    private void UpdateShellSprite(GameObject go, Crab crab)
    {
        spriteR = go.GetComponent<SpriteRenderer>();
        spriteR.sprite = crab.shell.sprite;
    }
}
