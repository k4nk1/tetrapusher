using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    [SerializeField]
    private float frequency;
    [SerializeField]
    private float amplitude;
    
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.velocity = Vector3.back * amplitude * Mathf.Sin(6.28f * frequency * Time.time);
    }
}
