using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class CanvasController : MonoBehaviour
{
    public static CanvasController instance = null;

    CanvasController()
    {
        instance = this;
    }
    PlayerIndex controllerNumber = PlayerIndex.One;
    PlayerIndex controllerNumber2 = PlayerIndex.Two;
    PlayerIndex controllerNumber3 = PlayerIndex.Three;
    PlayerIndex controllerNumber4 = PlayerIndex.Four;
    GamePadState gpState;
    GamePadState gpState2;
    GamePadState gpState3;
    GamePadState gpState4;
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

    public bool up_a = false;

    public void OnRestartPressed(){
        SceneManager.LoadScene("ConceptScene");
    }

    public void OnExitPressed()
    {
        SceneManager.LoadScene("Menu");
    }


    void Start()
    {
        gpState = GamePad.GetState(controllerNumber);
        if (gpState.IsConnected)
            Debug.Log("SIII!!");
        else
            Debug.Log("NOOO!!");
        gpState2 = GamePad.GetState(controllerNumber2);
        if (gpState2.IsConnected)
            Debug.Log("SIII!!");
        else
            Debug.Log("NOOO!!");
        gpState3 = GamePad.GetState(controllerNumber3);
        if (gpState3.IsConnected)
            Debug.Log("SIII!!");
        else
            Debug.Log("NOOO!!");
        gpState4 = GamePad.GetState(controllerNumber4);
        if (gpState4.IsConnected)
            Debug.Log("SIII!!");
        else
            Debug.Log("NOOO!!");
        cd_3.enabled = false;
        cd_2.enabled = false;
        cd_1.enabled = false;
        cd_out.enabled = false;
		one = false;
     	two = false;
		three = false;
     four = false;  

		go.enabled = false;
        info.enabled = false;
        restart.enabled = false;
        exit.enabled = false;

        tuto1.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        gpState = GamePad.GetState(controllerNumber);
        gpState2 = GamePad.GetState(controllerNumber2);
        gpState3 = GamePad.GetState(controllerNumber3);
        gpState4 = GamePad.GetState(controllerNumber4);
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
                this.GetComponent<AudioSource>().pitch = 0.5f;
                this.GetComponent<AudioSource>().Play();
                this.GetComponent<AudioSource>().pitch = 1;
                four = true;
                cd_1.enabled = false;
                go.enabled = true;
                go.GetComponent<Animation>().Play();
            }
            else
                    if (start_timer > 2.12f && !cd_1.enabled && !three)
            {
                this.GetComponent<AudioSource>().Play();
                cd_1.GetComponent<Animation>().Play();
                three = true;
                cd_2.enabled = false;
                cd_1.enabled = true;

            }
            else
                        if (start_timer > 1.15f && !cd_2.enabled && !two)
            {
                this.GetComponent<AudioSource>().Play();
                cd_2.GetComponent<Animation>().Play();
                two = true;
                cd_3.enabled = false;
                cd_2.enabled = true;

            }
            else
                            if (start_timer > 0.0f && !cd_3.enabled && !one)
            {
                cd_3.enabled = true;
                this.GetComponent<AudioSource>().Play();
                cd_3.GetComponent<Animation>().Play();
                one = true;
            }
        }

        if (gpState.Buttons.A == ButtonState.Released)
            up_a = false;

        if (!up_a && tuto2.active && gpState.Buttons.A == ButtonState.Pressed || Input.GetKeyDown("space"))
        {
            // TODO: GUILLERMO BOOL;
            tuto2.active = false;
            start = true;
            
        }
        if(tuto1.active && gpState.Buttons.A == ButtonState.Pressed || Input.GetKeyDown("space")){
            tuto1.active = false;
            tuto2.active = true;
            up_a = true;
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
			if (gpState.Buttons.A == ButtonState.Pressed || Input.GetKeyDown("up"))
            {
                OnRestartPressed();
            }
			if (gpState.Buttons.B == ButtonState.Pressed || Input.GetKeyDown("down"))
            {
                OnExitPressed();
            }
        }

		if(camera_controller.timer == 0.00f && one && !start){
			cd_3.enabled = false;
			cd_2.enabled = false;
			cd_1.enabled = false;
			cd_out.enabled = false;
			one = false;
			two = false;
			three = false;
			four = false;  
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
