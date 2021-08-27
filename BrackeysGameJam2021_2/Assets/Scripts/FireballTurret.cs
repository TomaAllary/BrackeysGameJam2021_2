using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class FireballTurret : Turret
{
    // Start is called before the first frame update
    void Start()
    {
        range = Constants.CANONBALL_TURRET_BASIC_RANGE;
        attackRate = Constants.CANONBALL_TURRET_BASIC_SPEED;
        cooldown = 0f;
    }
    void Update()
    {
        //try to kill actual target if always in range
        if (target != null && target.currentHealth > 0 && ((target.gameObject.transform.position - projectileStartPos.position).magnitude < range))
        {
            if (cooldown < 0)
            {
                Projectile ammo = Instantiate(projectile);
                ammo.transform.position = projectileStartPos.position;
                ammo.target = target.transform;
                ammo.transform.LookAt(target.transform);

                //reset cooldown
                cooldown = attackRate;
            }
            else
            {
                cooldown -= Time.deltaTime;
            }
        }
        //Change target
        else
        {
            cooldown = 0f;
            Collider[] hitColliders = Physics.OverlapSphere(projectileStartPos.position, range, LayerMask.GetMask("Goat"));

            if (hitColliders.Length > 0)
                target = hitColliders[0].gameObject.GetComponent<Goat>();
        }
    }


}
