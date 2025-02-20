using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BodyDatabase : ScriptableObject
{
    public Body[] bodies;


    public int BodyCount
    {
        get { return bodies.Length; }
    }

    public Body GetBody(int index)
    {
        return bodies[index];
    }
}
