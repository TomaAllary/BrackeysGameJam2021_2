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
    private float targetTick;

    // Start is called before the first frame update
    void Start()
    {
        attackDamage = 5;
        isAttacking = true;
        attackRemainingCD = attackCooldown = 1;
        targetTick = 50;
        healthBar.GetComponent<HealthBar>().setMaxHealth(Constants.NORMAL_GOAT_HEALTH);
        currentHealth = Constants.NORMAL_GOAT_HEALTH;   
        
    }
    private void Awake()
    {
        
        agent = this.gameObject.GetComponentInChildren<NavMeshAgent>();
        goal = GameObject.Find("Player").transform;
        agent.destination = goal.position;

    }

    // Update is called once per frame
    void Update()
    {

        //GameObject turret = GameObject.Find("Turret(Clone)");
        //if(turret != null)
        //    agent.destination = turret.transform.position;
        //else
        //{
        //    agent.destination = goal.position;
        //}



        //attack
        if(attackRemainingCD < 0) {
            attackBox.enabled = true;
            if (attackRemainingCD < -(attackCooldown / 3)) {
                attackBox.enabled = false;
                attackRemainingCD = attackCooldown;
            }
        }
        attackRemainingCD -= Time.deltaTime;
        //if(targetTick > 0)
        //    targetTick--;
        //else
        //{
        //    targetTick = 50;
        //    findTarget(GameObject.Find("Player").transform);
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

    //private void findTarget(Transform goal)
    //{
    //    NavMeshPath path = new NavMeshPath();
    //    agent.CalculatePath(goal.position, path);
    //    if (path.status == NavMeshPathStatus.PathPartial)
    //    {
    //        if(goal.gameObject.name == "Player")
    //        {
    //            if(GameObject.Find("Turret(Clone)") == null)
    //            {
    //                goal = GameObject.Find("Wall").transform;
    //            }
    //            findTarget(goal);
    //        }
    //        else
    //        {
    //            goal = GameObject.Find("Wall(Clone)").transform;
    //            agent.destination = goal.position;
    //        }
           
    //    }
    //    agent.destination = goal.position;
    //}

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
