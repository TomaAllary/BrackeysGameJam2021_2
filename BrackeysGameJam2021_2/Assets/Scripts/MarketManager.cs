using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketManager : MonoBehaviour
{
    //singleton
    private static MarketManager instance;
    public static MarketManager Instance { get { return instance; } }

    private int[] ressources = new int[4];
    private Dictionary<string, int> itemsCost = new Dictionary<string, int>();

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
        }
    }

    public void AddRessource(int resType) {
        ressources[resType]++;
    }

    //public void BuyWoodWall()

}
