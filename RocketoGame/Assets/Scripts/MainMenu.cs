using UnityEngine;
using Cinemachine;

public class MainMenu : MonoBehaviour
{
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
    public Material mat;

    enum currentMenu { menu, goToCredits, credits, goToMenu };
    currentMenu state = currentMenu.menu;
    enum currentSubMenu { play, credits, quit };
    currentSubMenu subState = currentSubMenu.play;
    void Start()
    {
        dolly = virtualCameraPrefab.GetCinemachineComponent<CinemachineTrackedDolly>();
        audioSource = GetComponent<AudioSource>();
        mat.mainTexture = texture_play;
    }
    
    void Update()
    {
        switch(state)
        {
            case currentMenu.menu:
                
                switch (subState)
                {
                    case currentSubMenu.play:
                        if (Input.GetKeyDown(KeyCode.Space))
                            Application.LoadLevel("ConceptScene");
                        if (Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            subState = currentSubMenu.credits;
                            mat.mainTexture = texture_credits;
                            audioSource.Stop();
                            audioSource.PlayOneShot(clip);
                        }
                        else if (Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            subState = currentSubMenu.quit;
                            mat.mainTexture = texture_exit;
                            audioSource.Stop();
                            audioSource.PlayOneShot(clip);
                        }
                        break;
                    case currentSubMenu.credits:
                        if (Input.GetKeyDown(KeyCode.Space))
                            state = currentMenu.goToCredits;
                        if (Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            subState = currentSubMenu.quit;
                            mat.mainTexture = texture_exit;
                            audioSource.Stop();
                            audioSource.PlayOneShot(clip);
                        }
                        else if(Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            subState = currentSubMenu.play;
                            mat.mainTexture = texture_play;
                            audioSource.Stop();
                            audioSource.PlayOneShot(clip);
                        }
                        break;
                    case currentSubMenu.quit:
                        if (Input.GetKeyDown(KeyCode.Space))
                            Application.Quit();
                        if (Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            subState = currentSubMenu.play;
                            mat.mainTexture = texture_play;
                            audioSource.Stop();
                            audioSource.PlayOneShot(clip);
                        }
                        else if(Input.GetKeyDown(KeyCode.UpArrow))
                        {
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
                if (Input.GetKeyDown(KeyCode.Space))
                    state = currentMenu.goToMenu;
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
