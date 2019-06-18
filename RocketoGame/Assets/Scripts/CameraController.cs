using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player1;
    public Transform player2;

    public float smooth_speed = 0.125f;
    public Vector3 offset;

    private Transform current_winner;

    // Start is called before the first frame update
    void Start()
    {
        current_winner = player1;
    }

	private void Update()
	{
        if(Input.GetKeyDown("space")){
            current_winner = player2;
        }

        if (Input.GetKeyDown("up"))
        {
            current_winner = player1;
        }

	}

	void FixedUpdate()
    {
        Vector3 desired_position = current_winner.position + offset;
        Vector3 smoothed_position = Vector3.Lerp(transform.position, desired_position, smooth_speed);
        transform.position = smoothed_position;

        //transform.LookAt(current_target);
    }
}
