using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Canonball : Projectile
{
    public GameObject fireballExplosion;
    private float lifeTick;
    private float ghostTick;
    // Start is called before the first frame update
    void Start()
    {
        isAttacking = true;
        attackDamage = Constants.CANONBALL_BASIC_ATTACK;
        push = Constants.CANONBALL_BASIC_PUSH;
        lifeTick = 7;
        speed = Constants.CANONBALL_BASIC_SPEED;
        gameObject.GetComponent<Collider>().enabled = false;
        ghostTick = 0.5f;
    }

    private void FixedUpdate()
    {
        transform.position = (transform.position + transform.forward * Time.fixedDeltaTime * speed);
        lifeTick -= Time.deltaTime;
        if (lifeTick <= 0)
            Destroy(gameObject);
        if (ghostTick > 0)
        {
            ghostTick -= Time.deltaTime;
        }
        else
        {
            gameObject.GetComponent<Collider>().enabled = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Transform current = gameObject.transform;
        Instantiate(fireballExplosion, current.position, current.rotation);
        Destroy(this.gameObject);

    }
}
