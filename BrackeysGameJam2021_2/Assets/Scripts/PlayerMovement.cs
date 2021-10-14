using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject craftingPanel;
    [SerializeField] private GameObject forkTurnPoint;
    public float speed = 10.00f;
    public Camera mainCamera;
    public int maxHealth;
    public int playerHealth;

    private GameObject player;
    public GameObject Staff;
    public GameObject fireball;
    public HealthBar healthBar;
    private Vector3 direction;
    private float horizontalInput;
    private float verticalInput;
    private bool canAttack;
    private float attackCoolDown;
    private float fireballCoolDown;
    public AudioClip step;
    public AudioClip stick;
    private float steptick;

    private float fireballTimer;

    private bool basicAttacking;
    private float basicAttackAngle;
 

    private Rigidbody rb;
    private NavMeshAgent agent;

    private Vector3 spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        spawnPos = transform.position;

        basicAttacking = false;

        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        canAttack = true;
        attackCoolDown = 0f;
        fireballCoolDown = Constants.FIREBALL_COOLDOWN;
        fireballTimer = fireballCoolDown;

        playerHealth = Constants.NORMAL_PLAYER_HEALTH;
        maxHealth = playerHealth;
        healthBar.setMaxHealth(playerHealth);
        steptick = 0;
    }

    private void Update()
    {
        //toggle craft menu on E
        if (Input.GetKeyDown(KeyCode.E)) {
            craftingPanel.SetActive(!craftingPanel.activeSelf);
            if (craftingPanel.activeSelf == false)
                MarketManager.Instance.ClearObjectToPlace();
        }

        else if (Input.GetKey(KeyCode.Mouse0) && canAttack) {
            attackCoolDown = 0.5f;
            canAttack = false;
            gameObject.GetComponent<AudioSource>().PlayOneShot(stick);
            Staff.GetComponent<Staff>().isAttacking = true;
            basicAttacking = true;
            basicAttackAngle = 0f;
        }

        else if (Input.GetKeyDown(KeyCode.Space) && fireballTimer < 0)
        {
            Fireball ball = Instantiate(fireball, (transform.position + (transform.forward * 5)), transform.rotation).GetComponent<Fireball>();

            fireballTimer = fireballCoolDown;
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

        if (basicAttacking) {
            basicAttackAngle += 360 * 4 * Time.deltaTime;
            transform.Rotate(new Vector3(0, 360 * 4 * Time.deltaTime, 0));

            if(basicAttackAngle >= 360) {
                basicAttacking = false;
                Staff.GetComponent<Staff>().isAttacking = false;
            }
        }

        fireballTimer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        var cameraForward = mainCamera.transform.forward;
        var cameraRight = mainCamera.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if ((horizontalInput != 0 || verticalInput != 0) && !basicAttacking)
        {
            direction = horizontalInput * cameraRight + verticalInput * cameraForward;
            transform.LookAt(transform.position + direction);
            //transform.position = (transform.position + direction * Time.fixedDeltaTime * speed);

            //rb.MovePosition(transform.position + (direction * Time.fixedDeltaTime * speed));
            agent.Move(direction * Time.fixedDeltaTime * speed);
            if (steptick <= 0)
            {
                gameObject.GetComponentInChildren<AudioSource>().PlayOneShot(step);
                steptick = .4f;
            }               
        }

        if (steptick > 0)
            steptick -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Loot"))
        {
            Destroy(other.gameObject);
            MarketManager.Instance.AddRessource(other.gameObject.tag);
            DataFile.stats[other.gameObject.tag]++;
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("Goat") && other.GetComponent<Weapon>() != null)
        {
            playerHealth = playerHealth - other.GetComponent<Weapon>().attackDamage;
            healthBar.setHealth(playerHealth);
            if (playerHealth <= 0)
            {
                transform.position = spawnPos;
                playerHealth = maxHealth;
                healthBar.setHealth(playerHealth);

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


    public void UpgradeMaxHealth(UpgradeBar action) {
        if (action.Upgrade("Horn:1")) {
            float healthRatio = (float)healthBar.getMaxHealth() / (float)healthBar.getHealth();
            healthBar.setMaxHealth((int)(healthBar.getMaxHealth() * 1.1f));
            healthBar.setHealth((int)(healthBar.getMaxHealth() * healthRatio));
        }
    }

    public void UpgradeMovementSpeed(UpgradeBar action) {
        if (action.Upgrade("Horn:1")) {
            speed += 2.5f;
        }
    }

    public void UpgradeAttackDmg(UpgradeBar action) {
        if (action.Upgrade("Horn:1")) {
            Staff.GetComponent<Staff>().attackDamage = (int)(1.5 * Staff.GetComponent<Staff>().attackDamage);
        }
    }

    public void UpgradeFireballRate(UpgradeBar action) {
        if (action.Upgrade("Horn:1")) {
            fireballCoolDown *= 0.7f;
        }
    }

    public void UpgradeFireballExplosion(UpgradeBar action) {
        if (action.Upgrade("Horn:1")) {
            Constants.FIREBALL_EXPLOSION_MAX_SIZE = Constants.FIREBALL_EXPLOSION_MAX_SIZE * 1.1f;
            Constants.FIREBALL_EXPLOSION_BASIC_PUSH = Constants.FIREBALL_EXPLOSION_BASIC_PUSH * 1.04f;
        }
    }

    public void UpgradeFireballDmg(UpgradeBar action) {
        if (action.Upgrade("Horn:1")) {
            Constants.FIREBALL_BASIC_ATTACK = (int)(Constants.FIREBALL_BASIC_ATTACK * 1.1f);
        }
    }
}