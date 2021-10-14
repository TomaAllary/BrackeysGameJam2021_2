using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour
{
    private Transform player;

    private float distanceToOpen = 3.0f;
    private GameObject renderObj;
    private BoxCollider doorCollider;
    private NavMeshObstacle navMeshCollider;
    // Start is called before the first frame update
    void Start()
    {
        renderObj = gameObject.transform.GetChild(0).gameObject;
        doorCollider = GetComponent<BoxCollider>();
        navMeshCollider = GetComponent<NavMeshObstacle>();
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if(Mathf.Abs((transform.position - player.position).magnitude) <= distanceToOpen) {
            renderObj.SetActive(false);
            doorCollider.enabled = false;
            navMeshCollider.enabled = false;
        }
        else {
            renderObj.SetActive(true);
            doorCollider.enabled = true;
            navMeshCollider.enabled = true;
        }
    }

}
