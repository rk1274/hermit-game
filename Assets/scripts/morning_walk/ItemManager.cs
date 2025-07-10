using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour
{
    public ItemDatabase itemDB;
    public ChosenItems chosenItems;

    public Button[] buttons;

    private Dictionary<Button, Item> butDict;

    private int itemCount;

    public PlayerInventory inv;

    private int internalCrabs;

    public GameObject morning_walk;
    public GameObject new_crab;

    public NewCrabManager crabManager;

    public int crabStatus = 0;

    public move_camera cam;


    private void Start()
    {
        butDict = new Dictionary<Button, Item>(itemDB.ItemCount);
        itemCount = 0;

        internalCrabs = inv.CrabCount;

        chosenItems.ClearAllItems();

        foreach (Button button in buttons)
        {
            butDict.Add(button, UpdateButton(button));
        }
    }

    private int RandomNum()
    {
        float num = Random.Range(0, itemDB.ActiveItemCount);
        return System.Convert.ToInt32(num);
    }
     
    private Item UpdateButton(Button curButton)
    {
        Item curItem = itemDB.GetActiveItem(RandomNum());
        
        curButton.GetComponent<Image>().sprite = curItem.sprite;

        return curItem;
    }

    public void AddNewSelection(Button button)
    {
        Item butItem = butDict[button];
        chosenItems.SetItem(butItem, itemCount);
        itemCount++;
    }

    public void SetChosen(GameObject buttonObj)
    {

        Button button = buttonObj.GetComponent<Button>();
        StartCoroutine(SetItem(button, buttonObj));
    }

    public IEnumerator SetItem(Button button, GameObject buttonObj)
    {

        Debug.Log("ITEM COUNT:"+itemCount);
        if (itemCount < 5)
        {
            Item butItem = butDict[button];
            string[] name = butItem.name.Split('_');

            if (name[0] == "crab" && inv.Capacity <= internalCrabs)
            {
                Debug.Log("no space for crab");
            }
            else if (name[0] == "crab")
            {
                buttonObj.SetActive(false);
                chosenItems.SetCrab(butItem);
                morning_walk.SetActive(false);
                new_crab.SetActive(true);


                int speed = cam.speed;
                cam.speed = 0;

                Vector3 prevPosition = Camera.main.transform.position;
                cam.transform.position = new Vector3(0, 0, -10);
                crabManager.SetCrab();

                yield return new WaitUntil(() => crabStatus != 0);

                if (crabStatus == 1)
                {
                    crabStatus = 0;
                    cam.speed = speed;
                    cam.transform.position = prevPosition;
                    morning_walk.SetActive(true);
                    new_crab.SetActive(false);
                }
                else if (crabStatus == 2)
                {
                    crabStatus = 0;
                    internalCrabs++;
                    cam.speed = speed;
                    cam.transform.position = prevPosition;
                    morning_walk.SetActive(true);
                    new_crab.SetActive(false);

                    AddNewSelection(button);
                }
                else
                {
                    throw new System.Exception();
                }

            }
            else
            {
                buttonObj.SetActive(false);
                AddNewSelection(button);
            }

        }

    }

}
