using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMaker : MonoBehaviour
{
    public SpawnZone[] spawnPoints;
    public Goat goat;
    public float timer;
    public int wave;
    public PlayerMovement player;
    public AudioClip goatScream;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        timer = 20;
        gameObject.GetComponent<Timer>().startTimer(timer);
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
            timer = 60;
        }
        else if (wave == 1)
        {
            spawnWave(30, goat);
            timer = 120;
        }
        wave++;
        gameObject.GetComponent<Timer>().startTimer(timer);
    }
    public void spawnWave(int number, Goat goat)
    {
        player.playerHealth = player.maxHealth;
        player.healthBar.setHealth(player.maxHealth);
        if (wave != 0)
            gameObject.GetComponentInChildren<AudioSource>().PlayOneShot(goatScream);
        foreach (SpawnZone s in spawnPoints)
        {          
            StartCoroutine(s.spawnGoats(number, goat));
        }
    }
}
