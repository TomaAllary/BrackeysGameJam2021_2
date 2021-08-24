using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Fireball : Weapon
{
    public float speed = Constants.FIREBALL_BASIC_SPEED;
    // Start is called before the first frame update
    void Start()
    {       
        isAttacking = true;
        attackDamage = Constants.FIREBALL_BASIC_ATTACK;
    }

    private void FixedUpdate()
    {
        transform.position = (transform.position + transform.forward * Time.fixedDeltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
