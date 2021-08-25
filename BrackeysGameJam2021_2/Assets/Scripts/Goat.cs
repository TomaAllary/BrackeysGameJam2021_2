using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Goat : Weapon
{
    public GameObject healthBar;
    public int currentHealth;
    public Transform goal;
    public BoxCollider attackBox;

    private NavMeshAgent agent;
    private float attackCooldown;
    private float attackRemainingCD;

    // Start is called before the first frame update
    void Start()
    {
        attackDamage = 5;
        isAttacking = true;
        attackRemainingCD = attackCooldown = 1;

        healthBar.GetComponent<HealthBar>().setMaxHealth(Constants.NORMAL_GOAT_HEALTH);
        currentHealth = Constants.NORMAL_GOAT_HEALTH;   
        
    }
    private void Awake()
    {
        goal = GameObject.Find("Player").transform;
        agent = this.gameObject.GetComponentInChildren<NavMeshAgent>();
        agent.destination = goal.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    currentHealth = currentHealth - 20;
        //    healthBar.GetComponent<HealthBar>().setHealth(currentHealth);
        //    if (currentHealth <= 0)
        //    {
        //        Destroy(this.gameObject);
        //    }
        //}
        GameObject turret = GameObject.Find("Turret(Clone)");
        if(turret != null)
            agent.destination = turret.transform.position;
        else
        {
            agent.destination = goal.position;
        }

        //attack
        if(attackRemainingCD < 0) {
            attackBox.enabled = true;
            if (attackRemainingCD < -(attackCooldown / 3)) {
                attackBox.enabled = false;
                attackRemainingCD = attackCooldown;
            }
        }
        attackRemainingCD -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Weapon") && other.GetComponent<Weapon>().isAttacking)
        {
            currentHealth = currentHealth - other.GetComponent<Weapon>().attackDamage;
            healthBar.GetComponent<HealthBar>().setHealth(currentHealth);
            if(currentHealth <= 0)
            {
                Destroy(this.gameObject);
            }        
        }
    }

    //public void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Weapon") && collision.gameObject.GetComponent<Weapon>().isAttacking)
    //    {
    //        currentHealth = currentHealth - collision.gameObject.GetComponent<Weapon>().attackDamage;
    //        healthBar.GetComponent<HealthBar>().setHealth(currentHealth);
    //        if (currentHealth <= 0)
    //        {
    //            Destroy(this.gameObject);
    //        }
    //    }
    //}
}
