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
    private float stunTick;
    private float gravTick;


    // Start is called before the first frame update
    void Start()
    {
        attackDamage = 5;
        isAttacking = true;
        attackRemainingCD = attackCooldown = 1;
        healthBar.GetComponent<HealthBar>().setMaxHealth(Constants.NORMAL_GOAT_HEALTH);
        currentHealth = Constants.NORMAL_GOAT_HEALTH;
        stunTick = 0;
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

        if(gameObject.transform.position.y < -10)
        {
            Destroy(gameObject);
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

        if (stunTick > 0)
            stunTick-= Time.deltaTime;
        else
        {
            try
            {
                if (agent.enabled == false)
                    agent.enabled = true;              
                agent.destination = goal.position;
            }
            //Means the goat is oob
            catch
            {
                Destroy(gameObject);
            }
        }
        if (gravTick > 0)
        {
            gravTick -= Time.deltaTime;
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
            

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Weapon") && other.GetComponent<Weapon>().isAttacking)
    //    {
    //        currentHealth = currentHealth - other.GetComponent<Weapon>().attackDamage;
    //        healthBar.GetComponent<HealthBar>().setHealth(currentHealth);
    //        if(currentHealth <= 0)
    //        {
    //            Destroy(this.gameObject);
    //        }
    //        agent.enabled = false;
    //        stunTick = other.GetComponent<Weapon>().attackDamage * 30;
    //        Vector3 dir = other.transform.position - transform.position;
    //        dir = -dir.normalized;
    //        dir = (dir * other.GetComponent<Weapon>().push);
    //        if (dir.y > 0)
    //            dir.y = -dir.y;
    //        gameObject.GetComponent<Rigidbody>().AddForce(dir, ForceMode.VelocityChange);
    //    }

    //}

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

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Weapon") && collision.gameObject.GetComponent<Weapon>().isAttacking)
        {
            currentHealth = currentHealth - collision.gameObject.GetComponent<Weapon>().attackDamage;
            healthBar.GetComponent<HealthBar>().setHealth(currentHealth);
            if (currentHealth <= 0)
            {
                Destroy(this.gameObject);
            }
            agent.enabled = false;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            stunTick = collision.gameObject.GetComponent<Weapon>().push *2;
            gravTick = collision.gameObject.GetComponent<Weapon>().push/3;

            //Vector3 dir = collision.contacts[0].point - transform.position;
            //dir = -dir.normalized;
            //dir = (dir * collision.gameObject.GetComponent<Weapon>().push + Vector3.up * collision.gameObject.GetComponent<Weapon>().push * 10);
            Vector3 dir = Vector3.up * collision.gameObject.GetComponent<Weapon>().push;
            gameObject.GetComponent<Rigidbody>().AddForce(dir, ForceMode.Impulse);
            //gameObject.GetComponent<Rigidbody>().AddForce(dir, ForceMode.VelocityChange);
        }

    }

    //IEnumerator FakeAddForceMotion(Rigidbody rb, Vector3 dir)
    //{
    //    float i = 0.01f;
    //    while (100 > i)
    //    {
    //        rb.gameObject.GetComponent<Rigidbody>().velocity = dir; 
    //        i = i + Time.deltaTime;
    //        //yield return new WaitForEndOfFrame();
    //    }
    //    rb.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //    yield return null;
    //}
}
