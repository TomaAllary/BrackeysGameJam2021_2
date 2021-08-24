using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Goat : MonoBehaviour
{
    public GameObject healthBar;
    public int currentHealth;
    public Transform goal;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.GetComponent<HealthBar>().setMaxHealth(Constants.NORMAL_GOAT_HEALTH);
        currentHealth = Constants.NORMAL_GOAT_HEALTH;   
        goal = GameObject.Find("Player").transform;
    }
    private void Awake()
    {
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

        agent.destination = goal.position;
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
