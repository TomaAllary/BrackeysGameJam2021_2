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
    public AudioClip countDown;
    public AudioClip mainTheme;
    public AudioClip waveTheme;
    public AudioSource mainSource;

    // Start is called before the first frame update
    void Start()
    {

   
    }

    private void Awake()
    {
        timer = 20;
        wave = 0;
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        gameObject.GetComponent<Timer>().startTimer(timer);
        mainSource = GameObject.Find("Ambiance").GetComponentInChildren<AudioSource>();
        mainSource.PlayOneShot(mainTheme);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer > 3 && timer < 4)
                gameObject.GetComponent<AudioSource>().PlayOneShot(countDown);
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
        gameObject.GetComponent<Timer>().startTimer(timer);
        wave++;
    }
    public void spawnWave(int number, Goat goat)
    {
        player.playerHealth = player.maxHealth;
        DataFile.nbWaves = wave;
        player.healthBar.setHealth(player.maxHealth);        
        //gameObject.GetComponentInChildren<AudioSource>().PlayOneShot(goatScream);
        mainSource.Stop();
        mainSource.PlayOneShot(waveTheme);
        foreach (SpawnZone s in spawnPoints)
        {          
            StartCoroutine(s.spawnGoats(number, goat));
        }
    }
}
