using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShellDatabase : ScriptableObject
{
    [SerializeField] private Shell[] shells;

    public int ShellCount => shells.Length;
    public Shell GetShell(int index) => shells[index];
}
