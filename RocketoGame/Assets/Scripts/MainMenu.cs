using UnityEngine;
using Cinemachine;
using XInputDotNetPure;

public class MainMenu : MonoBehaviour
{
    PlayerIndex controllerNumber = PlayerIndex.One;
    PlayerIndex controllerNumber2 = PlayerIndex.Two;
    PlayerIndex controllerNumber3 = PlayerIndex.Three;
    PlayerIndex controllerNumber4 = PlayerIndex.Four;
    GamePadState gpState;
    GamePadState gpState2;
    GamePadState gpState3;
    GamePadState gpState4;
    public CinemachineVirtualCamera virtualCameraPrefab;
    public Transform credits;
    public Transform menu;
    public float _ConstantForce = 1.0f;
    CinemachineTrackedDolly dolly;

    public Texture texture_play;
    public Texture texture_credits;
    public Texture texture_exit;
    AudioSource audioSource;
    public AudioClip clip;
    public AudioClip clipPress;
    public Material mat;

    bool pressed = false;

    bool downPlay = false;
    bool upPlay = false;

    enum currentMenu { menu, goToCredits, credits, goToMenu };
    currentMenu state = currentMenu.menu;
    enum currentSubMenu { play, credits, quit };
    currentSubMenu subState = currentSubMenu.play;
    void Start()
    {
        dolly = virtualCameraPrefab.GetCinemachineComponent<CinemachineTrackedDolly>();
        audioSource = GetComponent<AudioSource>();
        mat.mainTexture = texture_play;

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
    }
    
    void Update()
    {
        gpState = GamePad.GetState(controllerNumber);
        gpState2 = GamePad.GetState(controllerNumber2);
        gpState3 = GamePad.GetState(controllerNumber3);
        gpState4 = GamePad.GetState(controllerNumber4);
        switch (state)
        {
            case currentMenu.menu:
                
                switch (subState)
                {
                    case currentSubMenu.play:
                        if (gpState.DPad.Down == ButtonState.Released)
                        {
                            downPlay = false;
                        }
                        if (gpState.DPad.Up == ButtonState.Released)
                        {
                            upPlay = false;
                        }
                        if (gpState.Buttons.A == ButtonState.Pressed || Input.GetKeyDown(KeyCode.Space))
                        {
                            // TODO: GUILLERMO BOOL;
                            // start game
                            audioSource.PlayOneShot(clipPress);
                            Application.LoadLevel("ConceptScene");
                        }
                        if ((!downPlay && gpState.DPad.Down == ButtonState.Pressed) || Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            downPlay = true;
                            subState = currentSubMenu.credits;
                            mat.mainTexture = texture_credits;
                            audioSource.Stop();
                            audioSource.PlayOneShot(clip);
                        }
                        else if (( !upPlay && gpState.DPad.Up == ButtonState.Pressed) || Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            upPlay = true;
                            subState = currentSubMenu.quit;
                            mat.mainTexture = texture_exit;
                            audioSource.Stop();
                            audioSource.PlayOneShot(clip);
                        }
                        break;
                    case currentSubMenu.credits:
                        if (gpState.DPad.Down == ButtonState.Released)
                        {
                            downPlay = false;
                        }
                        if (gpState.DPad.Up == ButtonState.Released)
                        {
                            upPlay = false;
                        }
                        if (gpState.Buttons.A == ButtonState.Pressed || Input.GetKeyDown(KeyCode.Space))
                        {
                            // credits
                            audioSource.PlayOneShot(clipPress);
                            state = currentMenu.goToCredits;
                        }
                        if ((!downPlay && gpState.DPad.Down == ButtonState.Pressed) || Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            downPlay = true;
                            subState = currentSubMenu.quit;
                            mat.mainTexture = texture_exit;
                            audioSource.Stop();
                            audioSource.PlayOneShot(clip);
                        }
                        else if((!upPlay && gpState.DPad.Up == ButtonState.Pressed) || Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            upPlay = true;
                            subState = currentSubMenu.play;
                            mat.mainTexture = texture_play;
                            audioSource.Stop();
                            audioSource.PlayOneShot(clip);
                        }
                        break;
                    case currentSubMenu.quit:
                        if (gpState.DPad.Down == ButtonState.Released)
                        {
                            downPlay = false;
                        }
                        if (gpState.DPad.Up == ButtonState.Released)
                        {
                            upPlay = false;
                        }
                        if (gpState.Buttons.A == ButtonState.Pressed || Input.GetKeyDown(KeyCode.Space))
                        {
                            // quit
                            audioSource.PlayOneShot(clipPress);
                            Application.Quit();
                        }
                        if ((!downPlay && gpState.DPad.Down == ButtonState.Pressed) || Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            downPlay = true;
                            subState = currentSubMenu.play;
                            mat.mainTexture = texture_play;
                            audioSource.Stop();
                            audioSource.PlayOneShot(clip);
                        }
                        else if((!upPlay && gpState.DPad.Up == ButtonState.Pressed) || Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            upPlay = true;
                            subState = currentSubMenu.credits;
                            mat.mainTexture = texture_credits;
                            audioSource.Stop();
                            audioSource.PlayOneShot(clip);
                        }
                        break;
                }

                break;
            case currentMenu.goToCredits:
                dolly.m_PathPosition += Time.deltaTime * _ConstantForce;

                if (dolly.m_PathPosition >= 2)
                {
                   // virtualCameraPrefab.LookAt = credits;
                    //virtualCameraPrefab.m_LookAt = credits;
                }
                if (dolly.m_PathPosition >= 4)
                {
                 
                    dolly.m_PathPosition = 4;
                    state = currentMenu.credits;
                }
                break;
            case currentMenu.credits:
                if (gpState.Buttons.B == ButtonState.Pressed || Input.GetKeyDown(KeyCode.Space))
                {
                    state = currentMenu.goToMenu;
                    audioSource.PlayOneShot(clipPress);
                }
                    break;
            case currentMenu.goToMenu:
                dolly.m_PathPosition -= Time.deltaTime * _ConstantForce;
                if (dolly.m_PathPosition <= 2)
                {
                  //  virtualCameraPrefab.LookAt =
                   // virtualCameraPrefab.m_LookAt = menu;
                }
                if (dolly.m_PathPosition <= 0)
                {
                    
                    dolly.m_PathPosition = 0;
                    state = currentMenu.menu;
                }
                break;
        }
    }
}
