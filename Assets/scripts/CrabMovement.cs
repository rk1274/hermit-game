using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CrabMovement : MonoBehaviour
{
	[SerializeField] private float startDelay = 0f;
    [SerializeField] private float repeatDelay = 1f;
    [SerializeField] private float moveStep = 0.1f;
    [SerializeField] private float maxDistance = 0.4f;

    private Vector3[] moveDirections;
    private int currentMoveDirection;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;

        moveDirections = new Vector3[]
        {
            Vector3.right * moveStep,
            (Vector3.up + Vector3.left) * (moveStep * 0.5f),
            Vector3.down * moveStep,
            Vector3.zero
        };

        InvokeRepeating(nameof(Move), startDelay, repeatDelay);
    }

    private void Move()
    {
        ChooseMoveDirection();

        Vector3 proposedPosition = transform.position + moveDirections[currentMoveDirection];

        if (Vector3.Distance(startPosition, proposedPosition) <= maxDistance)
        {
            transform.position = proposedPosition;
        }
    }

    private void ChooseMoveDirection()
    {
        currentMoveDirection = Random.Range(0, moveDirections.Length);
    }
}