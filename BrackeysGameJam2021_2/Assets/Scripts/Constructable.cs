using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constructable : Upgradable
{
    public HealthBar healthBar;
    public Billboard billboard;
    public int currentHealth;
    public AudioClip woodDestruct;
    public AudioClip stoneDestruct;

    public string MaxHealthUpgradeCost;
    public string AttackDmgUpgradeCost;
    public string FireRateUpgradeCost;

    public int MaxHealthLevel;
    public int AttackDmgLevel;
    public int FireRateLevel;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = gameObject.GetComponentInChildren<HealthBar>();
        billboard = gameObject.GetComponentInChildren<Billboard>();
        healthBar.setMaxHealth(currentHealth);

        AttackDmgLevel = MaxHealthLevel = FireRateLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void UpgradeMaxHealth(UpgradeBar upgradeBar) {
        if (upgradeBar.Upgrade(MaxHealthUpgradeCost)) {
            healthBar.setMaxHealth((int)(healthBar.getMaxHealth() * 1.1f));

            //Refill full life
            healthBar.setHealth((int)(healthBar.getMaxHealth()));
            currentHealth = (int)(healthBar.getMaxHealth());

            MaxHealthLevel++;
        }
    }

    public override void UpgradeAttackDmg(UpgradeBar upgradeBar) {
        if (upgradeBar.Upgrade(AttackDmgUpgradeCost)) {
            Turret turret = gameObject.GetComponent<Turret>();
            if(turret != null) {
                int dmgToAdd = (int)(turret.attackDmg * 1.1f);
                if (dmgToAdd == 0)
                    dmgToAdd = 1;
                turret.attackDmg += dmgToAdd;

                AttackDmgLevel++;
            }
        }
    }

    public override void UpgradeFireRate(UpgradeBar upgradeBar) {
        if (upgradeBar.Upgrade(FireRateUpgradeCost)) {
            Turret turret = gameObject.GetComponent<Turret>();
            if (turret != null) {
                turret.attackRate *= 0.8f;
                FireRateLevel++;
            }
        }
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
                DataFile.nbDestroyed++;
            }
        }
    }

    private void OnDestroy() {
        if (MarketManager.Instance.selectedItem == this)
            MarketManager.Instance.selectedItem = null;
        MarketManager.Instance.RefreshItemUpgradePanel();
    }
}
