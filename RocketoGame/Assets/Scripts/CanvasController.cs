using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasController : MonoBehaviour
{
    public CameraController camera_controller;
    public TextMeshProUGUI cd_3;
    public TextMeshProUGUI cd_2;
    public TextMeshProUGUI cd_1;
    public TextMeshProUGUI cd_out;

    private bool one = false;
    private bool two = false;
    private bool three = false;
    private bool four = false;
    // Start is called before the first frame update
    void Start()
    {
        cd_3.enabled = false;
        cd_2.enabled = false;
        cd_1.enabled = false;
        cd_out.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(camera_controller.timer);
        if(camera_controller.timer > 4.05f){
            cd_out.enabled = false;

        }else
            if (camera_controller.timer > 3.15f&& !cd_out.enabled && !four)
        {
            four = true;
            cd_1.enabled = false;
            cd_out.enabled = true;
            cd_out.GetComponent<Animation>().Play();
        }
        else
                if (camera_controller.timer > 2.12f&& !cd_1.enabled && !three)
        {
            three = true;
            cd_2.enabled = false;
            cd_1.enabled = true;
            cd_1.GetComponent<Animation>().Play();
        }
        else
                    if (camera_controller.timer > 1.15f&& !cd_2.enabled && !two)
        {
            two = true;
            cd_3.enabled = false;
            cd_2.enabled = true;
            cd_2.GetComponent<Animation>().Play();
        }
        else
        if (camera_controller.timer > 0.0f && !cd_3.enabled && !one)
        {
            cd_3.enabled = true;
            cd_3.GetComponent<Animation>().Play();
            one = true;
        }
    }
}
