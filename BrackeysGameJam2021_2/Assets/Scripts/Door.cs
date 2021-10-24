using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private float openDoorTick;
    // Start is called before the first frame update
    void Start()
    {
        openDoorTick = 0;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (openDoorTick > 0)
        {
            openDoorTick -= Time.deltaTime;
        }
        else
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            openDoorTick = 1;
        }
    }
}
