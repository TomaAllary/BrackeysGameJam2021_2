using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject craftingPanel;
    public Vector3 cameraPosition;
    public float speed = 8.00f;
    public Dictionary<string, int> inventory = new Dictionary<string, int>();
    public Camera mainCamera;
    public TMP_Text woodText;
    public TMP_Text rockText;
    private GameObject player;

    private Vector3 direction;
    private float horizontalInput;
    private float verticalInput;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
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


        }

        //toggle craft menu on E
        if (Input.GetKeyDown(KeyCode.E)) {
            craftingPanel.SetActive(!craftingPanel.activeSelf);
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
            woodText.text = "Wood: " + value;
            inventory.TryGetValue("Rock", out value);
            rockText.text = "Rock: " + value;
        }
    }
}