using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyableItem : MonoBehaviour
{
    [SerializeField] private TMP_Text label;

    public string ressourceType;
    public int cost;
    public string marketName;

    private void Start() {


        label.text = marketName + ": " + cost + " " + ressourceType;
    }




}
