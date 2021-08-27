using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonballExplosion : FireballExplosion
{
    // Start is called before the first frame update
    void Start()
    {
        attackDamage = Constants.CANONBALL_BASIC_ATTACK;
        push = Constants.CANONBALL_EXPLOSION_BASIC_PUSH;
        gameObject.GetComponent<Transform>().localScale = gameObject.GetComponent<Transform>().localScale * Constants.CANONBALL_EXPLOSION_MAX_SIZE;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
