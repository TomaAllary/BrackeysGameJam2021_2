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
    public GameObject branch;
    public GameObject stone;
    public GameObject horn;
    private NavMeshAgent agent;
    private float attackCooldown;
    private float attackRemainingCD;
    private float stunTick;
    private float gravTick;
    private float deathCounter;
    private bool isDying;
    private Color color;
    public AudioClip[] goatCries;
    public AudioClip hit;


    // Start is called before the first frame update
    void Start()
    {
        attackDamage = 5;
        isAttacking = true;
        attackRemainingCD = attackCooldown = 1;
        healthBar.GetComponent<HealthBar>().setMaxHealth(Constants.NORMAL_GOAT_HEALTH);
        currentHealth = Constants.NORMAL_GOAT_HEALTH;
        stunTick = 0;
        isDying = false;
        color = this.GetComponent<MeshRenderer>().material.color;
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
                if (agent.enabled == false && isDying == false)
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
        if(deathCounter > 0)
        {
            deathCounter -= Time.deltaTime;
        }
        else if (isDying)
        {
            StartCoroutine(lastMethod());           
            //color.a -= Time.deltaTime;
            //this.GetComponent <MeshRenderer>().material.color = color;
        }

        int cry = Random.Range(0, 50000);
        if (cry == 50)
        {
            int selectCry = Random.Range(0, goatCries.Length);
            gameObject.GetComponent<AudioSource>().PlayOneShot(goatCries[selectCry]);
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
            if(collision.gameObject.name == "Staff")
            {
                gameObject.GetComponent<AudioSource>().PlayOneShot(hit);
            }
            currentHealth = currentHealth - collision.gameObject.GetComponent<Weapon>().attackDamage;
            healthBar.GetComponent<HealthBar>().setHealth(currentHealth);
            if (currentHealth <= 0)
            {
                Quaternion rot = gameObject.transform.rotation;
                rot.z = -90;
                gameObject.transform.rotation = rot;
                gameObject.GetComponent<Rigidbody>().freezeRotation = true;
                // Doesn't work for some reason
                //color.a = 0.5f;
                //gameObject.GetComponent<MeshRenderer>().material.color = color;
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
                deathCounter = 5f;
                isDying = true;
            }
            agent.enabled = false;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            stunTick = collision.gameObject.GetComponent<Weapon>().push *2;
            gravTick = collision.gameObject.GetComponent<Weapon>().push/3;
            Vector3 dir = Vector3.up * collision.gameObject.GetComponent<Weapon>().push;
            gameObject.GetComponent<Rigidbody>().AddForce(dir, ForceMode.Impulse);
        }
    }
    
    private void spawnLoot(Vector3 pos, Quaternion rot)
    {
        int chance = Random.Range(0, 2);
        if (chance == 1)
        {
            int loot = Random.Range(0, 10);
            if (loot < 6)
            {
                 Instantiate(branch, pos, rot);
            }
            else if (loot < 9)
            {
                Instantiate(stone, pos, rot);
            }
            else
            {
                Instantiate(horn, pos, rot);
            }
        }
    }

    IEnumerator lastMethod()
    {
        Vector3 pos = gameObject.transform.position;
        pos.y = 1;
        Quaternion rot = gameObject.transform.rotation;
        DataFile.nbGoats++;
        Destroy(this.gameObject);
        spawnLoot(pos, rot);
        yield return null;
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
