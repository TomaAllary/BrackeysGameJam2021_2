using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MarketManager : MonoBehaviour
{
    [SerializeField] private List<BuyableItem> marketItems;

    [Header("wood, rock, ll and horn counter")]
    [SerializeField] private TMP_Text[] ressourceCounters = new TMP_Text[4];


    //singleton
    private static MarketManager instance;
    public static MarketManager Instance { get { return instance; } }

    private Dictionary<string, int> ressources = new Dictionary<string, int>() {
        { "Wood", 0 },
        { "Rock", 0 },
        { "LapisLazulis", 0 },
        { "Horn", 0 }
    };


    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
        }
    }

    public void Buy(BuyableItem buyableItem) {
        if(ressources[buyableItem.ressourceType] >= buyableItem.cost) {
            ressources[buyableItem.ressourceType] -= buyableItem.cost;

            ressourceCounters[GetRessourceTypeNumber(buyableItem.ressourceType)].text = buyableItem.ressourceType + ": " + ressources[buyableItem.ressourceType];
        }
    }

    public void SellRessource(string ressourceType) {

        //Sell ressource for LL **could have a multiplicator**
        if (ressources[ressourceType] > 0) {
            ressources[ressourceType]--;
            ressources["LapisLazulis"]++;

            ressourceCounters[GetRessourceTypeNumber(ressourceType)].text = ressourceType + ": " + ressources[ressourceType];
            ressourceCounters[Constants.LapisLazulis].text = "LL: " + ressources["LapisLazulis"];
        }


    }

    public void AddRessource(string type) {
        if (ressources.ContainsKey(type)) {
            ressources[type]++;

            ressourceCounters[GetRessourceTypeNumber(type)].text = type + ": " + ressources[type];
        }
    }

    public int GetRessourceTypeNumber(string ressourceName) {
        switch (ressourceName) {
            case "Wood":
                return Constants.Wood;

            case "Rock":
                return Constants.Rock;

            case "LapisLazulis":
                return Constants.LapisLazulis;

            case "Horn":
                return Constants.Horn;

        }
        return -1;
    }

}
