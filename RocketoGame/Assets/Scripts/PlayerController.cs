using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rowForce = 5f;
    public uint boatIndex = 0u;
    public float forceDistance = 1f;
    public float maxVelocity = 20f;
    public float minTriggerRange = 0.5f;
    public float torqueAngle = 45f;

    private bool rtDownPlayer1 = false;
    private bool ltDownPlayer1 = false;

    private bool rtDownPlayer2 = false;
    private bool ltDownPlayer2 = false;

    private Rigidbody rb;

    private Vector3 rtForceDir;
    private Vector3 ltForceDir;

    //TODO: ROW COOLDOWN

    private bool rightPressed1 = false;
    private bool rightPressed2 = false;
    private bool leftPressed1 = false;
    private bool leftPressed2 = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {


        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }

        InputManagement();
    }

    void InputManagement()
    {
        if(boatIndex == 0)
        {
            #region PLAYER 1 CONTROLS            
            if (Input.GetAxis("RightTrigger1") > minTriggerRange)
            {
                if (!rtDownPlayer1 && !ltDownPlayer1)
                {
                    OnRightTriggerPressed(1);
                }

                rtDownPlayer1 = true;
            }
            else
            {
                rtDownPlayer1 = false;
            }

            if (Input.GetAxis("LeftTrigger1") > minTriggerRange)
            {
                if (!ltDownPlayer1 && !rtDownPlayer1)
                {
                    OnLeftTriggerPressed(1);
                }
                ltDownPlayer1 = true;
            }
            else
            {
                ltDownPlayer1 = false;
            }
            #endregion

            #region PLAYER 2 CONTROLS            
            if (Input.GetAxis("RightTrigger2") > minTriggerRange)
            {
                if (!rtDownPlayer2 && !ltDownPlayer2)
                {
                    OnRightTriggerPressed(2);
                }

                rtDownPlayer2 = true;
            }
            else
            {
                rtDownPlayer2= false;
            }

            if (Input.GetAxis("LeftTrigger2") > minTriggerRange)
            {
                if (!ltDownPlayer2 && !rtDownPlayer2)
                {
                    OnLeftTriggerPressed(2);
                }
                ltDownPlayer2 = true;
            }
            else
            {
                ltDownPlayer2 = false;
            }
            #endregion
        }
        else
        {
            #region PLAYER 3 CONTROLS            
            if (Input.GetAxis("RightTrigger3") > minTriggerRange)
            {
                if (!rtDownPlayer1 && !ltDownPlayer1)
                {
                    OnRightTriggerPressed(3);
                }

                rtDownPlayer1 = true;
            }
            else
            {
                rtDownPlayer1 = false;
            }

            if (Input.GetAxis("LeftTrigger3") > minTriggerRange)
            {
                if (!ltDownPlayer1 && !rtDownPlayer1)
                {
                    OnLeftTriggerPressed(3);
                }
                ltDownPlayer1 = true;
            }
            else
            {
                ltDownPlayer1 = false;
            }
            #endregion

            #region PLAYER 4 CONTROLS            
            if (Input.GetAxis("RightTrigger4") > minTriggerRange)
            {
                if (!rtDownPlayer2 && !ltDownPlayer2)
                {
                    OnRightTriggerPressed(4);
                }

                rtDownPlayer2 = true;
            }
            else
            {
                rtDownPlayer2 = false;
            }

            if (Input.GetAxis("LeftTrigger4") > minTriggerRange)
            {
                if (!ltDownPlayer2 && !rtDownPlayer2)
                {
                    OnLeftTriggerPressed(4);
                }
                ltDownPlayer2 = true;
            }
            else
            {
                ltDownPlayer2 = false;
            }
            #endregion
        }

    }

    //Player index: {1, 2, 3, 4}
    void OnRightTriggerPressed(uint playerIndex)
    {
        //TODO: APPLY FORCES AND SYNCRONIZE WITH THE ANIMS
        Debug.Log("Right: Player " + playerIndex);

        rtForceDir = (Quaternion.AngleAxis(-torqueAngle, Vector3.up) * transform.forward).normalized;
        rb.AddForceAtPosition(rtForceDir * rowForce, transform.position + transform.forward * forceDistance, ForceMode.Impulse);
    }

    void OnLeftTriggerPressed(uint playerIndex)
    {
        //TODO: APPLY FORCES AND SYNCRONIZE WITH THE ANIMS
        Debug.Log("Left: Player " + playerIndex);

        ltForceDir = (Quaternion.AngleAxis(torqueAngle, Vector3.up) * transform.forward).normalized;
        rb.AddForceAtPosition(ltForceDir * rowForce, transform.position + transform.forward * forceDistance, ForceMode.Impulse);
    }
}
