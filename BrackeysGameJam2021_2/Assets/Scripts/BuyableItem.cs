using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyableItem : MonoBehaviour
{
    [SerializeField] private TMP_Text label;
    [SerializeField] private Image UI_Image;

    public BuyableItem(string marketName, string ressourceType, int cost, Sprite marketImage) {
        this.MarketName = marketName;
        this.RessourceType = ressourceType;
        this.Cost = cost;
        this.MarketImage = marketImage;
        switch (ressourceType) {
            case "Wood":
                break;
            case "Rock":
                RessourceTypeNumber = Constants.Rock;
                break;
            case "LapisLazulis":
                RessourceTypeNumber = Constants.LapisLazulis;
                break;
            case "Horn":
                RessourceTypeNumber = Constants.Horn;
                break;
        }
    }

    public string RessourceType { get; set; }
    public int RessourceTypeNumber { get; set; }
    public int Cost { get; set; }
    public string MarketName { get; set; }
    public Sprite MarketImage { get; set; }

    private void Start() {
        UI_Image.sprite = MarketImage;

        label.text = MarketName + ": " + Cost + " " + RessourceType;
    }




}
