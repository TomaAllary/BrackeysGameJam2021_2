using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FireballExplosion : Weapon
{
    private int existancetick = 10;
    // Start is called before the first frame update
    void Start()
    {
        isAttacking = true;
        attackDamage = Constants.FIREBALL_BASIC_ATTACK;
        push = Constants.FIREBALL_EXPLOSION_BASIC_PUSH;
    }

    // Update is called once per framesaaaaaaa
    void Update()
    {
        if (existancetick <= 1)
            Destroy(this.gameObject);
        else
            existancetick--;
    }
}
