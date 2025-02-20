using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CrabDatabase : ScriptableObject
{
    public Crab[] crabs;


    public int crabCount
    {
        get { return crabs.Length; }
    }

    public Crab GetCrab(string name)
    {
        foreach(Crab crab in crabs)
        {
            if(crab.name == name)
            {
                return crab;
            }
        }
        return null;
    }

}
