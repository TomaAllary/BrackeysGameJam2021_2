using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Fireball : Projectile
{    
    public GameObject fireballExplosion;
    private float lifeTick;
    // Start is called before the first frame update
    void Start()
    {       
        isAttacking = true;
        attackDamage = Constants.FIREBALL_BASIC_ATTACK;
        push = Constants.FIREBALL_BASIC_PUSH;
        lifeTick = 7;
        speed = Constants.FIREBALL_BASIC_SPEED;
    }

    private void FixedUpdate()
    {
        transform.position = (transform.position + transform.forward * Time.fixedDeltaTime * speed);
        lifeTick -= Time.deltaTime;
        if (lifeTick <= 0)            
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        Transform current = gameObject.transform;
        Instantiate(fireballExplosion, current.position, current.rotation);
        Destroy(this.gameObject);
        
    }
}
