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

    public GameObject wood;
    public GameObject rock;
    public Vector3 maxMapPoint;
    public Vector3 maxSpawnPoint;
    public Vector3 minSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {

   
    }

    private void Awake()
    {
        timer = 30;
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

        if(Random.Range(0,50) == 1) {
            var obj = Instantiate(wood);
            obj.transform.position = new Vector3(Random.Range(minSpawnPoint.x, maxSpawnPoint.x), 1, Random.Range(minSpawnPoint.z, maxSpawnPoint.z));
        }

        if(Random.Range(0,100) == 1) {
            var obj = Instantiate(rock);
            obj.transform.position = new Vector3(Random.Range(minSpawnPoint.x, maxSpawnPoint.x), 1, Random.Range(minSpawnPoint.z, maxSpawnPoint.z));
        }
    }

    public void timesUp()
    {
        if (wave == 0) 
        {
            spawnWave(20, goat);
            timer = 120;
        }
        else
        {
            spawnWave(20 + wave*10, goat);
            timer = 180;
        }

        gameObject.GetComponent<Timer>().startTimer(timer);
        wave++;
    }
    public void spawnWave(int number, Goat goat)
    {
        player.playerHealth = player.maxHealth;
        DataFile.stats["nbWaves"] = wave;
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
