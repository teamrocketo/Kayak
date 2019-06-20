using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictorySoundController : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip audioClip;
    public CameraController cameraController;
    private bool played = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraController.game_end && !played)
        {
            audioSource.PlayOneShot(audioClip);
            played = true;
        }
    }
}
