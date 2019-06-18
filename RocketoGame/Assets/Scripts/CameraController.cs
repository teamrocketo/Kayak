
﻿
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player1;
    public Transform player2;

    public float smooth_speed = 0.125f;
    public float max_distance_between_players = 20.0f;
    public Vector3 offset;

    public Transform current_winner;
    public Transform current_loser;

    public float distance_between_players;
    public Vector3 v_winner;

    [Range(0.33f, 0.5f)]
    public float offset_multipler = 0.33f;

    private float min_offser_multipler = 0.33f;
    private float max_offser_multipler = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        //offset.Set(0.0f, this.transform.position.y, this.transform.position.z);

        current_winner = player1;

        current_loser = player2;
    }

    public void SetFirstPlayer(GameObject first_player){
        if(first_player.tag == "player1"){
            current_winner = player1;
            current_loser = player2;
        }else{
            current_winner = player2;
            current_loser = player1;
        }
    }

	private void Update()
	{
        

        // Calculate the 1/3 distance between the 1st and 2nd place
        distance_between_players = Vector3.Distance(player1.position, player2.position);

        if (max_distance_between_players < distance_between_players)
            distance_between_players = max_distance_between_players;


        offset_multipler = -0.0085f * distance_between_players + max_offser_multipler;

        /*if (distance_between_players < 10.0f)
        {
            offset_multipler = 0.5f;
        }
        else if (distance_between_players >= 10.0f)
            offset_multipler = 0.33f;*/

        v_winner = current_loser.position - current_winner.position;
        v_winner.Normalize();
        v_winner = current_winner.position + (v_winner * (offset_multipler * distance_between_players));

        float y_smooth_multiplier = 1.5f - offset_multipler;

        // Move the camera
        Vector3 lil_off = new Vector3(offset.x, offset.y * y_smooth_multiplier, offset.z * y_smooth_multiplier);
        Vector3 desired_position = v_winner + lil_off;
        Vector3 smoothed_position = Vector3.Lerp(transform.position, desired_position, smooth_speed);
        //Debug.Log(offset_multipler);
        //smoothed_position.Set(smoothed_position.x, smoothed_position.y * y_smooth_multiplier, smoothed_position.z* y_smooth_multiplier);

        transform.position = smoothed_position;


	}
}
