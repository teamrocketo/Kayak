using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public static CanvasController instance = null;

    CanvasController()
    {
        instance = this;
    }

    public CameraController camera_controller;
    public TextMeshProUGUI cd_3;
    public TextMeshProUGUI cd_2;
    public TextMeshProUGUI cd_1;
    public TextMeshProUGUI cd_out;

    public TextMeshProUGUI go;

    public TextMeshProUGUI info;
    public TextMeshProUGUI restart;
    public TextMeshProUGUI exit;

    public GameObject tuto1;
    public GameObject tuto2;

    private bool one = false;
    private bool two = false;
    private bool three = false;
    private bool four = false;

    private float timer = 0.0f;
    private float start_timer = 0.0f;
    public bool start = false;

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
        go.enabled = false;
        info.enabled = false;
        restart.enabled = false;
        exit.enabled = false;

        tuto1.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            start_timer += Time.deltaTime;
            if (start_timer > 4.05f)
            {
                go.enabled = false;
                start = false;
                one = false; two = false; three = false; four = false;
            }
            else
                if (start_timer > 3.12f && !go.enabled && !four)
            {
                four = true;
                cd_1.enabled = false;
                go.enabled = true;
                go.GetComponent<Animation>().Play();
            }
            else
                    if (start_timer > 2.12f && !cd_1.enabled && !three)
            {
                cd_1.GetComponent<Animation>().Play();
                three = true;
                cd_2.enabled = false;
                cd_1.enabled = true;

            }
            else
                        if (start_timer > 1.15f && !cd_2.enabled && !two)
            {
                cd_2.GetComponent<Animation>().Play();
                two = true;
                cd_3.enabled = false;
                cd_2.enabled = true;

            }
            else
                            if (start_timer > 0.0f && !cd_3.enabled && !one)
            {
                cd_3.enabled = true;
                cd_3.GetComponent<Animation>().Play();
                one = true;
            }
        }

        if (tuto2.active && Input.GetButtonDown("ControllerButtonA") || Input.GetKeyDown("space"))
        {
            tuto2.active = false;
            start = true;
        }
        if(tuto1.active && Input.GetButtonDown("ControllerButtonA") || Input.GetKeyDown("space")){
            tuto1.active = false;
            tuto2.active = true;
        }


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
            if (Input.GetButtonDown("ControllerButtonA"))
            {
                OnRestartPressed();
            }
            if (Input.GetButtonDown("ControllerButtonB"))
            {
                OnExitPressed();
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
            Debug.Log("cmooonnn");
            cd_3.enabled = true;
            cd_3.GetComponent<Animation>().Play();
            one = true;
        }
    }
}
