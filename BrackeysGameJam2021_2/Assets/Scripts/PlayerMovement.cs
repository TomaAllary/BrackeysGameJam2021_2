using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public float speed = 8.00f;
    //public float xRange = 100.00f;
    //public float zRange = 100.00f;


    private GameObject player;

    private Vector3 direction;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void FixedUpdate()
    {

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {        
            direction = (horizontalInput * Vector3.right + verticalInput * Vector3.forward).normalized;
            transform.LookAt(transform.position + direction);
            rb.MovePosition(transform.position + direction * Time.fixedDeltaTime * speed);
        }

    }
}