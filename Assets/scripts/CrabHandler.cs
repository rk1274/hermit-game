using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] crabGOs = new GameObject[5];
    [SerializeField] private PlayerInventory inventory;

    private void Start()
    {
        List<Crab> crabs = inventory.Crabs;

        for (int i = 0; i < crabs.Count && i < crabGOs.Length; i++)
        {
            Crab crab = crabs[i];

            Transform body = crabGOs[i].transform.Find("body");
            Transform shell = crabGOs[i].transform.Find("shell");

            if (body != null && shell != null)
            {
                SetSprite(body.gameObject, crab.Body.Sprite);
                SetSprite(shell.gameObject, crab.Shell.Sprite);
                crabGOs[i].SetActive(true);
            }
            else
            {
                Debug.LogWarning($"Missing 'body' or 'shell' child on crabGOs[{i}]");
            }
        }
    }

    private void SetSprite(GameObject go, Sprite sprite)
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
}
