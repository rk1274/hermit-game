using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class CrabItem
{
    public Crab crab;
    public int ID;
    public string name;

    public CrabItem(Crab crabObj, int id) {
        crab = crabObj;
        ID = id;
        name = "test crab name";
    }
}
