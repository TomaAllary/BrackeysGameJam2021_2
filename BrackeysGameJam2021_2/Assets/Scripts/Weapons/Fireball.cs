using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Fireball : Weapon
{
    public float speed = Constants.FIREBALL_BASIC_SPEED;
    public GameObject fireballExplosion;
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
        Transform current = gameObject.transform;
        Instantiate(fireballExplosion, current.position, current.rotation);
        Destroy(this.gameObject);
        
    }
}
