using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goat : MonoBehaviour
{
    public GameObject healthBar;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.GetComponent<HealthBar>().setMaxHealth(Constants.NORMAL_GOAT_HEALTH);
        currentHealth = Constants.NORMAL_GOAT_HEALTH;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentHealth = currentHealth - 20;
            healthBar.GetComponent<HealthBar>().setHealth(currentHealth);
            if (currentHealth <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
