﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player1;
    public Transform player2;

    public float smooth_speed = 0.125f;
    public float max_distance_between_players = 10.0f;
    public Vector3 offset;

    public Transform current_winner;
    public Transform current_loser;

    public float distance_between_players;
    public Vector3 v_winner;

    // Start is called before the first frame update
    void Start()
    {
        current_winner = player1;
        current_loser = player2;
    }

	private void Update()
	{
        if(Input.GetKeyDown("space")){
            current_winner = player2;
            current_loser = player1;
        }

        if (Input.GetKeyDown("up"))
        {
            current_winner = player1;
            current_loser = player2;
        }

        // Calculate the 1/3 distance between the 1st and 2nd place
        distance_between_players = Vector3.Distance(player1.position, player2.position);


        if (max_distance_between_players < distance_between_players)
            distance_between_players = max_distance_between_players;

        Debug.Log(distance_between_players);

        v_winner = current_loser.position - current_winner.position;
        v_winner.Normalize();
        v_winner = current_winner.position + (v_winner * (0.33f * distance_between_players));

        // Move the camera
        Vector3 desired_position = v_winner + offset;
        Vector3 smoothed_position = Vector3.Lerp(transform.position, desired_position, smooth_speed);
        transform.position = smoothed_position;
	}

	private void OnDrawGizmos()
	{
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(v_winner, 1);
	}
}
