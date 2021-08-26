using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon : MonoBehaviour
{
    public int attackDamage;
    public bool isAttacking;
    public float push;
   
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnCollisionEnter(Collision c)
    //{
    //    if (c.gameObject.layer == LayerMask.NameToLayer("Goat"))
    //    {
    //        ////// Calculate Angle Between the collision point and the player
    //        //Vector3 dir = c.contacts[0].point - transform.position;
    //        ////// We then get the opposite (-Vector3) and normalize it
    //        //dir = -dir.normalized;
    //        ////// And finally we add force in the direction of dir and multiply it by force. 
    //        ////// This will push back the player
    //        //c.gameObject.GetComponent<Rigidbody>().AddForce(dir * push, ForceMode.Impulse);
    //        //////other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward + Vector3.up * push);
    //        ////c.gameObject.GetComponent<Rigidbody>().velocity = (dir);
    //    }
    //}
}
