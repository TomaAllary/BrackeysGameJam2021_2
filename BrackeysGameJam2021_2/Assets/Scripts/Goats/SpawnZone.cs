using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    float tick;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tick > 0)
            tick -= Time.deltaTime;
    }

    public IEnumerator spawnGoats(int number, Goat goat)
    {
        for (int i = 0; i < number; i++)
        {
            Instantiate(goat, gameObject.transform.position, gameObject.transform.rotation);
            tick = 0.5f;
            while (tick > 0)
                yield return null;
        }
    }
}
