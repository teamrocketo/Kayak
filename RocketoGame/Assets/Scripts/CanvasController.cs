using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public CameraController camera_controller;
    public TextMeshProUGUI cd_3;
    public TextMeshProUGUI cd_2;
    public TextMeshProUGUI cd_1;
    public TextMeshProUGUI cd_out;

    public TextMeshProUGUI info;
    public TextMeshProUGUI restart;
    public TextMeshProUGUI exit;

    private bool one = false;
    private bool two = false;
    private bool three = false;
    private bool four = false;

    private float timer = 0.0f;

    public void OnRestartPressed(){
        SceneManager.LoadScene("ConceptScene");
    }

    public void OnExitPressed()
    {
        SceneManager.LoadScene("Menu");
    }


    void Start()
    {
        cd_3.enabled = false;
        cd_2.enabled = false;
        cd_1.enabled = false;
        cd_out.enabled = false;
        info.enabled = false;
        restart.enabled = false;
        exit.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(camera_controller.game_end){
            timer += Time.deltaTime;
            if(timer > 2.0f && !info.enabled){
                if (camera_controller.winner == null)
                    return;
                if(camera_controller.winner.tag == "player1"){
                    info.SetText("YELLOW CREW WINS");
                }else{
                    info.SetText("GREEN CREW WINS");
                }

                info.enabled = true;
                restart.enabled = true;
                exit.enabled = true;
                info.GetComponent<Animation>().Play();
                restart.GetComponent<Animation>().Play();
                exit.GetComponent<Animation>().Play();
            }
        }


        if(camera_controller.timer > 4.05f){
            cd_out.enabled = false;
        }else
            if (camera_controller.timer > 3.12f&& !cd_out.enabled && !four)
        {
            four = true;
            cd_1.enabled = false;
            cd_out.enabled = true;
            cd_out.GetComponent<Animation>().Play();
        }
        else
                if (camera_controller.timer > 2.12f&& !cd_1.enabled && !three)
        {
                cd_1.GetComponent<Animation>().Play();
            three = true;
            cd_2.enabled = false;
            cd_1.enabled = true;
            
        }
        else
                    if (camera_controller.timer > 1.15f&& !cd_2.enabled && !two)
        {
                    cd_2.GetComponent<Animation>().Play();
            two = true;
            cd_3.enabled = false;
            cd_2.enabled = true;
            
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
