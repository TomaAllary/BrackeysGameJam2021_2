using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CantMissArrow : Projectile
{

    // Start is called before the first frame update
    void Start()
    {
        isAttacking = true;
        attackDamage = Constants.CANT_MISS_ARROW_ATTACK;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null) {
            //transform.LookAt(target.position, target.up);
            transform.rotation = Quaternion.LookRotation(transform.position - target.position);
            transform.position -= speed * Time.deltaTime * (transform.position - target.position).normalized;
        }
        else {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Goat"))
        {
            Destroy(gameObject);
        }
    }
}
