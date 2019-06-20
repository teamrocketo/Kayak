using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController1 : MonoBehaviour
{
    public CameraController camera_script;
    public bool set_first = true;

    private void OnTriggerEnter(Collider other)
    {
        if(set_first){
            set_first = false;
            camera_script.SetFirstPlayer(other.gameObject);
            camera_script.game_end = true;
        }
    }
}
