using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(0, 1)]
    public uint boatIndex = 0u;
    
    [Header("Row Attributes")]
    [Space(-2f)]
    public float rowForce = 5f;
    public float forceDistance = 1f;
    public float torqueAngle = 45f;

    [Space(6f)]
    [Tooltip("Cap the rigidbody velocity to a maximum value")]
    public float maxVelocity = 20f;
    [Tooltip("The minimum amount of pressure you have to set to the triggers to detect the key down (0->1)")]
    public float minTriggerRange = 0.5f;
    public float rowCooldown = 0.1f;
    public float rowAnimDuration = 0.3f;

    //Particles
    [Header("Particles References")]
    public List<ParticleSystem> particleSystems;

    public RowController rowControllerFront;
    public RowController rowControllerBack;

    public AudioClip splashFX;

    private AudioSource audioSource;

    //To imitate the KeyDown behavior
    private bool rtDownPlayer1 = false;
    private bool ltDownPlayer1 = false;
    private bool rtDownPlayer2 = false;
    private bool ltDownPlayer2 = false;

    private Rigidbody rb;

    private Vector3 rtForceDir;
    private Vector3 ltForceDir;

    //TODO: USE THIS BOOLS TO APPLY FORCES ON THE FIXEDUPDATE INSTEAD OF ON THE ONTRIGGER, IN THE UPDATE
    private bool rightPressed1 = false;
    private bool rightPressed2 = false;
    private bool leftPressed1 = false;
    private bool leftPressed2 = false;

    private float timeToTriggerP1 = 0f;
    private float timeToTriggerP2 = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
            if ((Input.GetAxis("RightTrigger1") > minTriggerRange || Input.GetKeyDown("right")))
            {
                if (timeToTriggerP1 < Time.time)
                {
                    if (!rtDownPlayer1 && !ltDownPlayer1)
                    {
                        OnRightTriggerPressed(1);
                        timeToTriggerP1 = Time.time + rowCooldown;
                    }
                }
                rtDownPlayer1 = true;
            }
            else
            {
                rtDownPlayer1 = false;
            }

            if ((Input.GetAxis("LeftTrigger1") > minTriggerRange|| Input.GetKeyDown("left")))
            {
                if(timeToTriggerP1 < Time.time)
                {
                    if (!ltDownPlayer1 && !rtDownPlayer1)
                    {
                        OnLeftTriggerPressed(1);
                        timeToTriggerP1 = Time.time + rowCooldown;
                    }
                }
                
                ltDownPlayer1 = true;
            }
            else
            {
                ltDownPlayer1 = false;
            }
            #endregion

            #region PLAYER 2 CONTROLS            
            if ((Input.GetAxis("RightTrigger2") > minTriggerRange))
            {
                if(timeToTriggerP2 < Time.time)
                {
                    if (!rtDownPlayer2 && !ltDownPlayer2)
                    {
                        OnRightTriggerPressed(2);
                        timeToTriggerP2 = Time.time + rowCooldown;
                    }
                }
                
                rtDownPlayer2 = true;
            }
            else
            {
                rtDownPlayer2= false;
            }

            if ((Input.GetAxis("LeftTrigger2") > minTriggerRange))
            {
                if(timeToTriggerP2 < Time.time)
                {
                    if (!ltDownPlayer2 && !rtDownPlayer2)
                    {
                        OnLeftTriggerPressed(2);
                        timeToTriggerP2 = Time.time + rowCooldown;
                    }
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
            if ((Input.GetAxis("RightTrigger3") > minTriggerRange|| Input.GetKeyDown("up")))
            {
                if(timeToTriggerP1 < Time.time)
                {
                    if (!rtDownPlayer1 && !ltDownPlayer1)
                    {
                        OnRightTriggerPressed(3);
                        timeToTriggerP1 = Time.time + rowCooldown;
                    }
                }
                
                rtDownPlayer1 = true;
            }
            else
            {
                rtDownPlayer1 = false;
            }

            if ((Input.GetAxis("LeftTrigger3") > minTriggerRange|| Input.GetKeyDown("down")))
            {
                if(timeToTriggerP1 < Time.time)
                {
                    if (!ltDownPlayer1 && !rtDownPlayer1)
                    {
                        OnLeftTriggerPressed(3);
                        timeToTriggerP1 = Time.time + rowCooldown;
                    }
                }
                
                ltDownPlayer1 = true;
            }
            else
            {
                ltDownPlayer1 = false;
            }
            #endregion

            #region PLAYER 4 CONTROLS            
            if ((Input.GetAxis("RightTrigger4") > minTriggerRange))
            {
                if(timeToTriggerP2 < Time.time)
                {
                    if (!rtDownPlayer2 && !ltDownPlayer2)
                    {
                        OnRightTriggerPressed(4);
                        timeToTriggerP2 = Time.time + rowCooldown;
                    }
                }
                
                rtDownPlayer2 = true;
            }
            else
            {
                rtDownPlayer2 = false;
            }

            if ((Input.GetAxis("LeftTrigger4") > minTriggerRange))
            {
                if(timeToTriggerP2 < Time.time)
                {
                    if (!ltDownPlayer2 && !rtDownPlayer2)
                    {
                        OnLeftTriggerPressed(4);
                        timeToTriggerP2 = Time.time + rowCooldown;
                    }
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

    private IEnumerator OnRowAnimFinished(uint playerIndex, bool right)
    {
        yield return new WaitForSeconds(rowAnimDuration);

        if(right)
        {
            rtForceDir = (Quaternion.AngleAxis(-torqueAngle, Vector3.up) * transform.forward).normalized;
            rb.AddForceAtPosition(rtForceDir * rowForce, transform.position + transform.forward * forceDistance, ForceMode.Impulse);

            if (playerIndex % 2 != 0)
            {
                particleSystems[0].Play();
            }
            else
            {
                particleSystems[2].Play();
            }

            
        }
        else
        {
            ltForceDir = (Quaternion.AngleAxis(torqueAngle, Vector3.up) * transform.forward).normalized;
            rb.AddForceAtPosition(ltForceDir * rowForce, transform.position + transform.forward * forceDistance, ForceMode.Impulse);

            if (playerIndex % 2 != 0)
            {
                particleSystems[1].Play();
            }
            else
            {
                particleSystems[3].Play();
            }
        }

        audioSource.clip = splashFX;
        audioSource.Play();
    }

    //Player index: {1, 2, 3, 4}
    void OnRightTriggerPressed(uint playerIndex)
    {
        //TODO: APPLY FORCES AND SYNCRONIZE WITH THE ANIMS
        //Debug.Log("Right: Player " + playerIndex);

        if (CanvasController.instance.start || CanvasController.instance.tuto1.active || CanvasController.instance.tuto2.active)
            return;

        if (playerIndex % 2 != 0)
        {
            rowControllerFront.OnRow(true);
        }
        else
        {
            rowControllerBack.OnRow(true);
        }

        StartCoroutine(OnRowAnimFinished(playerIndex, true));
    }

    void OnLeftTriggerPressed(uint playerIndex)
    {
        //TODO: APPLY FORCES AND SYNCRONIZE WITH THE ANIMS
        //Debug.Log("Left: Player " + playerIndex);

        if (CanvasController.instance.start || CanvasController.instance.tuto1.active || CanvasController.instance.tuto2.active)
            return;

        if (playerIndex % 2 != 0)
        {
            rowControllerFront.OnRow(false);
        }
        else
        {
            rowControllerBack.OnRow(false);
        }

        StartCoroutine(OnRowAnimFinished(playerIndex, false));
    }
}
