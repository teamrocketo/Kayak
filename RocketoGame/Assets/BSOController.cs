using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSOController : MonoBehaviour
{
    private AudioClip currentBSO;
    public AudioClip BSO;
    public AudioClip victoryMusic;
    public CameraController cameraControl;
    private AudioSource audioSource;

    private bool swithcMusic = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraControl.game_end && !swithcMusic)
        {
            currentBSO = victoryMusic;
            swithcMusic = true;
        }
        else if (!cameraControl.game_end && !swithcMusic)
        {
            currentBSO = BSO;
            swithcMusic = true;
        }

        if (swithcMusic)
        {
            swithcMusic = false;
            audioSource.clip = currentBSO;
            audioSource.Play();
        }
    }
}
