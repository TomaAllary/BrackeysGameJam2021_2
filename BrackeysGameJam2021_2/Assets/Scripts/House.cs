using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class House : MonoBehaviour
{
    public HealthBar healthBar;

    private int houseHealth;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.setMaxHealth(Constants.HOUSE_MAX_HEALTH);
        houseHealth = Constants.HOUSE_MAX_HEALTH;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetHealth() {
        houseHealth = Constants.HOUSE_MAX_HEALTH;
        healthBar.setHealth(houseHealth);
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Goat") && collision.gameObject.GetComponent<Weapon>().isAttacking)
        {
            houseHealth = houseHealth - collision.gameObject.GetComponent<Weapon>().attackDamage;
            healthBar.setHealth(houseHealth);
            if (houseHealth <= 0)
            {
                SceneManager.LoadScene("Outro");
            }
        }
    }
}
