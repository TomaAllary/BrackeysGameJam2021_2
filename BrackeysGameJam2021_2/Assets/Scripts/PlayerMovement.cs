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
    public Camera mainCamera;
    public int playerHealth;

    private GameObject player;
    public GameObject Staff;
    public GameObject fireball;
    private HealthBar healthBar;
    private Vector3 direction;
    private float horizontalInput;
    private float verticalInput;
    private bool canAttack;
    private float attackCoolDown;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
        canAttack = true;
        attackCoolDown = 0f;
        playerHealth = Constants.NORMAL_PLAYER_HEALTH;
        healthBar = gameObject.GetComponentInChildren<HealthBar>();
        healthBar.setMaxHealth(playerHealth);
    }

    private void Update()
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
            //transform.position = (transform.position + direction * Time.fixedDeltaTime * speed);

            rb.MovePosition(transform.position + direction * Time.fixedDeltaTime * speed);

        }

        //toggle craft menu on E
        if (Input.GetKeyDown(KeyCode.E)) {
            craftingPanel.SetActive(!craftingPanel.activeSelf);
        }

        else if (Input.GetKey(KeyCode.Mouse0) && canAttack) {
            attackCoolDown = 0.5f;
            canAttack = false;
            Staff.GetComponent<Staff>().isAttacking = true;          
        }

        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(fireball, (transform.position + (transform.forward * 5)), transform.rotation);
        }
        if (!canAttack)
        {
            if (attackCoolDown > 0)
                attackCoolDown -= Time.deltaTime;
            else
            {
                attackCoolDown = 0;
                canAttack = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Loot"))
        {
            Destroy(other.gameObject);
            MarketManager.Instance.AddRessource(other.gameObject.tag);
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("Goat"))
        {
            playerHealth = playerHealth - other.gameObject.GetComponent<Weapon>().attackDamage;
            healthBar.setHealth(playerHealth);
            if (playerHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
        
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Goat") && collision.gameObject.GetComponent<Weapon>().isAttacking)
    //    {
    //        playerHealth = playerHealth - collision.gameObject.GetComponent<Weapon>().attackDamage;
    //        healthBar.setHealth(playerHealth);
    //        if (playerHealth <= 0)
    //        {
    //            Destroy(gameObject);
    //        }
    //    }
    //}

}