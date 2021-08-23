using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketManager : MonoBehaviour
{
    [SerializeField] private GameObject marketScrollView;
    //singleton
    private static MarketManager instance;
    public static MarketManager Instance { get { return instance; } }

    private List<BuyableItem> marketItems = new List<BuyableItem>();
    private Dictionary<int, int> ressources = new Dictionary<int, int>() {
        { Constants.Wood, 0 },
        { Constants.Rock, 0 },
        { Constants.LapisLazulis, 0 },
        { Constants.Horn, 0 }
    };


    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
        }
    }

    public void AddBuyable(BuyableItem toAdd) {
        marketItems.Add(toAdd);


    }

    public void Buy(string buyableItemName) {

        foreach(BuyableItem buyable in marketItems) {
            if(buyable.MarketName == buyableItemName) {
                if(ressources[buyable.RessourceTypeNumber] >= buyable.Cost) {
                    ressources[buyable.RessourceTypeNumber] -= buyable.Cost;
                }
            }
        }
    }

}
