using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingCinemachine : MonoBehaviour
{
    Cinemachine.CinemachineVirtualCamera c_VirtualCamera;
    [SerializeField] Transform target;

    private void Awake()
    {
        c_VirtualCamera = GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space")){
            c_VirtualCamera.m_LookAt = target.transform;
            c_VirtualCamera.m_Follow = target.transform;
        }
    }
}
