using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rowForce = 10f;
    public uint boatIndex = 0u;

    private bool rtDownPlayer1 = false;
    private bool ltDownPlayer1 = false;

    private bool rtDownPlayer2 = false;
    private bool ltDownPlayer2 = false;

    private Rigidbody rb;

    private Vector3 rtForceDir;
    private Vector3 ltForceDir;

    //TODO: MAX VELOCITY
    //TODO: ROW COOLDOWN

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rtForceDir = (Quaternion.AngleAxis(-45, Vector3.up) * transform.forward).normalized;
        ltForceDir = (Quaternion.AngleAxis(45, Vector3.up) * transform.forward).normalized;
    }

    void Update()
    {
        InputManagement();

        Movement();
    }

    void InputManagement()
    {
        if(boatIndex == 0)
        {
            #region PLAYER 1 CONTROLS            
            if (Input.GetAxis("RightTrigger1") > 0)
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

            if (Input.GetAxis("LeftTrigger1") > 0)
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
            if (Input.GetAxis("RightTrigger2") > 0)
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

            if (Input.GetAxis("LeftTrigger2") > 0)
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
            if (Input.GetAxis("RightTrigger3") > 0)
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

            if (Input.GetAxis("LeftTrigger3") > 0)
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
            if (Input.GetAxis("RightTrigger4") > 0)
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

            if (Input.GetAxis("LeftTrigger4") > 0)
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

    void Movement()
    {




    }

    //Player index: {1, 2, 3, 4}
    void OnRightTriggerPressed(uint playerIndex)
    {
        //TODO: APPLY FORCES AND SYNCRONIZE WITH THE ANIMS
        Debug.Log("Right: Player " + playerIndex);

        rb.AddForce(rtForceDir * rowForce * Random.Range(0.0f,1.0f), ForceMode.Impulse);
    }

    void OnLeftTriggerPressed(uint playerIndex)
    {
        //TODO: APPLY FORCES AND SYNCRONIZE WITH THE ANIMS
        Debug.Log("Left: Player " + playerIndex);
        rb.AddForce(ltForceDir * rowForce, ForceMode.Impulse);
    }
}
