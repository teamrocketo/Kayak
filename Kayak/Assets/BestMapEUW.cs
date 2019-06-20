﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestMapEUW : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        if (Input.GetKey(KeyCode.S))
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        if (Input.GetKey(KeyCode.A))
            transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        if (Input.GetKey(KeyCode.D))
            transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);

    }
}
