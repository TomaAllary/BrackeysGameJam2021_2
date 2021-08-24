using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Projectile projectile;
    public Transform projectileStartPos;
    public float attackRate;
    public float range;

    private float cooldown;
    private Goat target;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //try to kill actual target if always in range
        if (target != null && target.currentHealth > 0 && ((target.gameObject.transform.position - projectileStartPos.position).magnitude < range)) {
            if (cooldown < 0) {
                Projectile ammo = Instantiate(projectile);
                ammo.transform.position = projectileStartPos.position;
                ammo.target = target.transform;

                //reset cooldown
                cooldown = attackRate;
            }
            else {
                cooldown -= Time.deltaTime;
            }
        }
        //Change target
        else {
            Collider[] hitColliders = Physics.OverlapSphere(projectileStartPos.position, range, LayerMask.GetMask("Goat"));

            if (hitColliders.Length > 0)
                target = hitColliders[0].gameObject.GetComponent<Goat>();
        }


        
    }
}
