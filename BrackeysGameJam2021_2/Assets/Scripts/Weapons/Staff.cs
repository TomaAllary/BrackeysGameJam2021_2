using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Staff : Weapon
{
    private float attackTimer;
    private bool timerStarted;
    // Start is called before the first frame update
    void Start()
    {
        attackDamage = Constants.STAFF_BASIC_ATTACK;
        isAttacking = false;
        timerStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        //This whole thing is so that goats won't be harmed by the staff unless it's attacking
        if(isAttacking == true)
        {
            if (!timerStarted)
            {
                timerStarted = true;
                attackTimer = 0.1f;
            }
            else
            {
                if (attackTimer > 0)
                    attackTimer -= Time.deltaTime;
                else
                {
                    attackTimer = 0;
                    isAttacking = false;
                    timerStarted = false;
                }
            }
        }
    }
}
