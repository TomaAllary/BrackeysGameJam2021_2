using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MonoBehaviour
{

    int torchTick = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (torchTick == 20)
        {
            float intensity = Random.Range(.4f, .7f);
            if (intensity < .45f)
                this.GetComponentInChildren<Light>().intensity = Random.Range(.1f, .45f);
            else
                this.GetComponentInChildren<Light>().intensity = intensity;
            torchTick = 0;
        }
        else
            torchTick++;
    }


}
