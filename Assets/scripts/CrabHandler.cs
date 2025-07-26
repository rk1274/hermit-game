using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrabHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] crabGOs = new GameObject[5];
    [SerializeField] private PlayerInventory inventory;

    private void Start()
    {
        Dictionary<int, Crab> crabs = inventory.Crabs;

        for (int i = 0; i < crabs.Count; i++) 
        {
            Crab crab = crabs.ElementAt(i).Value;
            int crabID = crabs.ElementAt(i).Key;

            Transform body = crabGOs[i].transform.Find("body");
            Transform shell = crabGOs[i].transform.Find("shell");

            Button button = crabGOs[i].GetComponentInChildren<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => LoadCrabInfo(crabID));
            }

            if (body != null && shell != null)
            {
                setSprite(body.gameObject, crab.Body.Sprite);
                setSprite(shell.gameObject, crab.Shell.Sprite);
                crabGOs[i].SetActive(true);
            }
            else
            {
                Debug.LogWarning($"Missing 'body' or 'shell' child on crabGOs[{i}]");
            }
        }
    }

    public void LoadCrabInfo(int crabID)
    {
        PlayerPrefs.SetInt ("selected_crab", crabID);
        SceneManager.LoadScene("crab_info");
    }

    private void setSprite(GameObject go, Sprite sprite)
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
