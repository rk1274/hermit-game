using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class crab_movement : MonoBehaviour
{

	int currentMoveDirection;

	internal Vector3[] moveDirections = new Vector3[] { Vector3.right/10, Vector3.up/10 + Vector3.left/10, Vector3.down/10, Vector3.zero/10 };
	// Use this for initialization

	public float startDelay = 0f;
	public float repeatDelay = 1f;

	Vector3 new_pos;

	float start_x;
	float start_y;

	float max_move = 0.4f;
	void Start()
	{
		start_x = transform.position.x;
		start_y = transform.position.y;

		InvokeRepeating("ExampleCoroutine", startDelay, repeatDelay);
	}


	void ExampleCoroutine()
	{

		ChooseMoveDirection();
		new_pos = transform.position + moveDirections[currentMoveDirection];
		if(new_pos.x <= start_x + max_move && new_pos.y <= start_y + max_move && new_pos.x >= start_x - max_move && new_pos.y >= start_y - max_move)
        {
			transform.position = new_pos;
        }
		//output to log the position change
		//Debug.Log(transform.position);
	}



	void ChooseMoveDirection()
	{
		// Choose whether to move sideways or up/down
		currentMoveDirection = Mathf.FloorToInt(Random.Range(0, moveDirections.Length));
	}

}
