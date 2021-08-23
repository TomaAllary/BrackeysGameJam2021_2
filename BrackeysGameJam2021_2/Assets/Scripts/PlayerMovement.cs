using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public Vector3 cameraPosition;
    public float speed = 8.00f;
    public Dictionary<string, int> inventory = new Dictionary<string, int>();
    public Camera mainCamera;
    public Text woodText;
    public Text rockText;
    private GameObject player;

    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void FixedUpdate()
    {
        var cameraForward = mainCamera.transform.forward;
        var cameraRight = mainCamera.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            direction = horizontalInput * cameraRight + verticalInput * cameraForward;
            transform.LookAt(transform.position + direction + cameraPosition);
            transform.position = (transform.position + direction * Time.fixedDeltaTime * speed);


            mainCamera.transform.position += direction * Time.fixedDeltaTime * speed;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Loot"))
        {
            Destroy(collider.gameObject);
            if (inventory.ContainsKey(collider.gameObject.tag))
            {
                int currentLevel = inventory[collider.gameObject.tag];
                inventory[collider.gameObject.tag] = currentLevel + 1;
                int token = 1;
            }
            else
            {
                inventory.Add(collider.gameObject.tag, 1);
                int Token = 1; 
            }
            int value;
            inventory.TryGetValue("Branch", out value);
            woodText.GetComponent<Text>().text = "Wood: " + value;
            inventory.TryGetValue("Rock", out value);
            rockText.GetComponent<Text>().text = "Rock: " + value;
        }
    }
}