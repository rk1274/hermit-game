using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BodyDatabase : ScriptableObject
{
    [SerializeField] private Body[] bodies;

    public int BodyCount => bodies.Length;
    public Body GetBody(int index) => bodies[index];
}
