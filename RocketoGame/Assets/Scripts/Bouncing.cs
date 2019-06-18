using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncing : MonoBehaviour
{
    [SerializeField]
    float height = 0.1f;
    [SerializeField]
    float amplitude = 1;

    private Vector3 initialPosition;
    private float offset;

    private void Awake()
    {
        initialPosition = transform.position;

        offset = 1 - (Random.value * 2);
    }

    private void Update()
    {
        transform.position = initialPosition - Vector3.up * Mathf.Sin((Time.time + offset) * amplitude) * height;
    }
}
