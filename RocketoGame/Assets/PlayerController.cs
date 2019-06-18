using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rowForce = 10f;

    private bool rightTriggerPressed = false;
    private bool leftTriggerPressed = false;

    void Update()
    {
        InputManagement();
        

    }

    void InputManagement()
    {
        if (Input.GetAxis("RightTrigger") > 0)
        {
            if (!rightTriggerPressed && !leftTriggerPressed)
            {
                OnRightTriggerPressed();
            }

            rightTriggerPressed = true;
        }
        else
        {
            rightTriggerPressed = false;
        }

        if (Input.GetAxis("LeftTrigger") > 0)
        {
            if (!leftTriggerPressed && !rightTriggerPressed)
            {
                OnLeftTriggerPressed();
            }
            leftTriggerPressed = true;
        }
        else
        {
            leftTriggerPressed = false;
        }
    }

    void OnRightTriggerPressed()
    {
        //TODO: APPLY FORCES AND SYNCRONIZE WITH THE ANIMS
    }

    void OnLeftTriggerPressed()
    {
        //TODO: APPLY FORCES AND SYNCRONIZE WITH THE ANIMS
    }
}
