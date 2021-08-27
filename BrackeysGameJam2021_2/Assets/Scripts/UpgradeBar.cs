using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBar : MonoBehaviour
{
    public int maxStack;
    public HealthBar pointBar;

    public int currentLevel;


    // Start is called before the first frame update
    void Start()
    {
        pointBar.setMaxHealth(maxStack);
        pointBar.setHealth(0);
        currentLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Upgrade(string cost) {
        if (currentLevel < maxStack) {
            MarketManager.Instance.SellRessourceWithoutRefund("Horn:" + cost);
            pointBar.setHealth(++currentLevel);
        }
    }
}