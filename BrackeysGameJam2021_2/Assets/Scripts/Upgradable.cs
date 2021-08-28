using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgradable : MonoBehaviour
{
    /*public BuyableItem buyableItem;
    public List<GameObject> levelingUpPrefabs;
    public List<GameObject> levelingUpPrefabsPreview;*/

    public bool canAttack;

    private int prefabLevel;


    // Start is called before the first frame update
    void Start()
    {
        prefabLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextLevelPrefab() {
        /*if (levelingUpPrefabs.Count > prefabLevel) {
            buyableItem.itemModel = levelingUpPrefabs[prefabLevel];
            buyableItem.itemPreview = levelingUpPrefabsPreview[prefabLevel];

            prefabLevel++;
        }*/
    }

    public virtual void UpgradeMaxHealth(UpgradeBar upgradeBar) {}
    public virtual void UpgradeAttackDmg(UpgradeBar upgradeBar) {}
    public virtual void UpgradeFireRate(UpgradeBar upgradeBar) {}
}
