using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CrabDatabase : ScriptableObject
{
    [SerializeField] private Crab[] crabs;

    public int crabCount => crabs.Length;

    public Crab GetCrab(string name)
    {
        foreach(Crab crab in crabs)
        {
            if(crab.Name == name)
            {
                return crab;
            }
        }

        return null;
    }
}
