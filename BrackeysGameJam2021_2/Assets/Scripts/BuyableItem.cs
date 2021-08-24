using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyableItem : MonoBehaviour
{
    public TMP_Text label;

    public GameObject itemPreview;
    public GameObject itemModel;

    public string ressourceType;
    public int cost;
    public string marketName;

    private void Start() {

        if(label != null)
            label.text = marketName + ": " + cost + " " + ressourceType;
    }




}
