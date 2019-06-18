using UnityEngine;
using Cinemachine;

public class MainMenu : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCameraPrefab;
    public Transform credits;
    public Transform menu;
    bool startBlend = false;
    public float _ConstantForce = 1.0f;
    CinemachineTrackedDolly dolly;

    enum currentMenu { menu, goToCredits, credits, goToMenu };
    currentMenu state = currentMenu.menu;
    void Start()
    {
        dolly = virtualCameraPrefab.GetCinemachineComponent<CinemachineTrackedDolly>();
    }
    
    void Update()
    {
        switch(state)
        {
            case currentMenu.menu:
                if (Input.GetKeyDown(KeyCode.Space))
                    state = currentMenu.goToCredits;
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
