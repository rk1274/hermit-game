using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShellDatabase : ScriptableObject
{
    public Shell[] shells;


    public int ShellCount
    {
        get { return shells.Length; }
    }

    public Shell GetShell(int index)
    {
        return shells[index];
    }
}
