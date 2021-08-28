using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constructable : MonoBehaviour
{
    public HealthBar healthBar;
    public Billboard billboard;
    public int currentHealth;
    public AudioClip woodDestruct;
    public AudioClip stoneDestruct;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = gameObject.GetComponentInChildren<HealthBar>();
        billboard = gameObject.GetComponentInChildren<Billboard>();
        healthBar.setMaxHealth(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Goat") && other.GetComponent<Weapon>().isAttacking) {
            currentHealth = currentHealth - other.GetComponent<Weapon>().attackDamage;
            healthBar.setHealth(currentHealth);
            if (currentHealth <= 0) {
                if (gameObject.CompareTag("wood"))
                {
                    GameObject.Find("ExplosionManager").GetComponent<AudioSource>().PlayOneShot(woodDestruct);
                }
                else if (gameObject.CompareTag("stone"))
                {
                    GameObject.Find("ExplosionManager").GetComponent<AudioSource>().PlayOneShot(stoneDestruct);
                }
                MarketManager.Instance.DestroyTileObject(this.gameObject);
            }
        }
    }
}
