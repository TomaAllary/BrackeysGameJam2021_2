using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Fireball : Weapon
{
    public float speed = Constants.FIREBALL_BASIC_SPEED;
    public GameObject fireballExplosion;
    private int lifeTick;
    // Start is called before the first frame update
    void Start()
    {       
        isAttacking = true;
        attackDamage = Constants.FIREBALL_BASIC_ATTACK;
        push = Constants.FIREBALL_BASIC_PUSH;
        lifeTick = 200;
    }

    private void FixedUpdate()
    {
        transform.position = (transform.position + transform.forward * Time.fixedDeltaTime * speed);
        lifeTick = lifeTick - 1;
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
