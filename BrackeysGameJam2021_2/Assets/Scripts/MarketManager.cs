using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class MarketManager : MonoBehaviour
{
    //CONSTRUCTION STUFF
    private GameObject objToPlace;
    private GameObject objPreview;

    private BuyableItem actualItemToPlace;

    [SerializeField] private Tilemap tileMap;
    [SerializeField] private List<Vector3> availablePlaces;

    //MARKET STUFF
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

    public bool Buy(BuyableItem buyableItem) {
        if(ressources[buyableItem.ressourceType] >= buyableItem.cost) {
            ressources[buyableItem.ressourceType] -= buyableItem.cost;

            ressourceCounters[GetRessourceTypeNumber(buyableItem.ressourceType)].text = buyableItem.ressourceType + ": " + ressources[buyableItem.ressourceType];
            return true;
        }
        else {
            return false;
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

    public void AddRessource(string type, int amount = 1) {
        if (ressources.ContainsKey(type)) {
            ressources[type] += amount;

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



    //--------------------------------------------------------------------------------------------//
    //
    //                      PLACING/CONSTUCTION CODE HERE....
    //
    //--------------------------------------------------------------------------------------------//
    //--------------------------------------------------------------------------------------------//



    void Start() {
        FindLocationsOfTiles();
        if (objPreview != null)
            objPreview = Instantiate(objPreview);
    }

    private void FindLocationsOfTiles() {
        availablePlaces = new List<Vector3>(); // create a new list of vectors by doing...

        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++) // scan from left to right for tiles
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++) // scan from bottom to top for tiles
            {
                Vector3Int localPlace = new Vector3Int(n, p, (int)tileMap.transform.position.y); // if you find a tile, record its position on the tile map grid

                if (tileMap.HasTile(localPlace)) {
                    //Tile at "place"
                    availablePlaces.Add(localPlace);
                }
                else {
                    //No tile at "place"
                }
            }
        }
    }


    private void Update() {

        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0) && objToPlace != null) {
            Camera cam = Camera.main;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Terrain"))) {

                Vector3 destination = new Vector3(hit.point.x, tileMap.transform.position.y, hit.point.z);
                Vector3Int gridPos = tileMap.WorldToCell(destination);
                SpawnObj(gridPos);
            }

        }

        if (objPreview != null) {
            Camera cam = Camera.main;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Terrain"))) {

                Vector3 destination = new Vector3(hit.point.x, tileMap.transform.position.y, hit.point.z);
                Vector3Int gridPos = tileMap.WorldToCell(destination);
                //if (availablePlaces.Contains(gridPos))
                    //play sound maybe

                objPreview.transform.position = tileMap.CellToWorld(gridPos);
            }
        }

        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetKey(KeyCode.X)) {
            Camera cam = Camera.main;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Construction"))) {

                BuyableItem itemDestroyed = hit.collider.gameObject.GetComponent<BuyableItem>();
                if (itemDestroyed != null) {
                    //refund the cost
                    AddRessource(itemDestroyed.ressourceType, itemDestroyed.cost);

                    //Remove object form unavaible place
                    Vector3 worldPosOnGrid = new Vector3(hit.collider.gameObject.transform.position.x, tileMap.transform.position.y, hit.collider.gameObject.transform.position.z);
                    Vector3Int gridPos = tileMap.WorldToCell(worldPosOnGrid);
                    availablePlaces.Remove(gridPos);

                    //Destroy object
                    Destroy(hit.collider.gameObject);
                }

            }
        }
    }

    public void SetObjectToPlace(BuyableItem buyableItem) {
        if (objPreview != null)
            Destroy(objPreview.gameObject);

        objPreview = Instantiate(buyableItem.itemPreview);
        objToPlace = buyableItem.itemModel;

        actualItemToPlace = buyableItem;
    }

    public void ClearObjectToPlace() {
        if (objPreview != null)
            Destroy(objPreview.gameObject);
        objPreview = null;
        objToPlace = null;
    }



    private void SpawnObj(Vector3Int pos) {

        if (availablePlaces.Contains(pos))
            return;

        Vector3 realPos = tileMap.CellToWorld(pos);

        if (Buy(actualItemToPlace)) {
            Instantiate(objToPlace, realPos, Quaternion.identity);
            availablePlaces.Add(pos);
        }

        

    }

}
