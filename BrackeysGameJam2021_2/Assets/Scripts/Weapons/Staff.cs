using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Staff : Weapon
{
    private float attackTimer;
    // Start is called before the first frame update
    void Start()
    {
        attackDamage = Constants.STAFF_BASIC_ATTACK;
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
