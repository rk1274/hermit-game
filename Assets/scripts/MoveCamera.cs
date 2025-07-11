using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private int speed = 10;

    public int Speed
    {
        get => speed;
        set => speed = value;
    }

    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }
}
