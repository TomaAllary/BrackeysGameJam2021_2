using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMaker : MonoBehaviour
{
    public SpawnZone[] spawnPoints;
    public Goat goat;
    public float timer;
    public int wave;
    // Start is called before the first frame update
    void Start()
    {
        timer = 10;
        wave = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
            timesUp();
    }

    public void timesUp()
    {
        if (wave == 0)
        {
            spawnWave(20, goat);
            timer = 30;
        }
        else if (wave == 1)
        {
            spawnWave(30, goat);
            timer = 60;
        }
        wave++;
    }
    public void spawnWave(int number, Goat goat)
    {
        foreach (SpawnZone s in spawnPoints)
        {          
            StartCoroutine(s.spawnGoats(number, goat));
        }
    }
}
