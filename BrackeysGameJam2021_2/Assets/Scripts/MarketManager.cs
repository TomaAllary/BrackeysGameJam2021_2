using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.AI;
using UnityEngine.UI;

public class MarketManager : MonoBehaviour
{
    //CONSTRUCTION STUFF
    private GameObject objToPlace;
    private GameObject objToPlacePreview;
    private GameObject objSelectedPreview;

    public Camera healthBarsCam;
    public GameObject upgradePanel;

    public TMP_Text itemSelectedLabel;
    public GameObject healthUpgradeBar;
    public GameObject dmgUpgradeBar;
    public GameObject fireRateUpgradeBar;

    private Constructable actualItemToPlace;
    public Constructable selectedItem;

    [SerializeField] public NavMeshSurface[] navMeshSurfaces;
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private List<Vector3> availablePlaces;
    [SerializeField] private GameObject BuyResPanel;


    [Header("wood, rock, ll and horn counter")]
    [SerializeField] private TMP_Text[] ressourceCounters = new TMP_Text[4];


    //singleton
    private static MarketManager instance;
    public static MarketManager Instance { get { return instance; } }
    //TODO: put these to 0, 99 is for testing only
    private Dictionary<string, int> ressources = new Dictionary<string, int>() {
        { "Wood", 29 },
        { "Rock", 19 },
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
        AddRessource("Wood");
        AddRessource("Rock");
    }

    public bool Buy(Constructable buyableItem) {
        if(ressources[buyableItem.ressourceType] >= buyableItem.cost) {
            ressources[buyableItem.ressourceType] -= buyableItem.cost;

            ressourceCounters[GetRessourceTypeNumber(buyableItem.ressourceType)].text = buyableItem.ressourceType + ": " + ressources[buyableItem.ressourceType];
            return true;
        }
        else {
            return false;
        }
    }

    //format: ressourceType:amount **amount is 1 if no string after :**
    public void SellRessource(string ressourceType) {
        string[] split = ressourceType.Split(':');
        ressourceType = split[0];
        int amount = 1;
        if (split.Length > 1)
            amount = int.Parse(split[1]);

        //Sell ressource for LL **could have a multiplicator**
        if (ressources[ressourceType] - amount >= 0) {
            ressources[ressourceType] -= amount;
            ressources["LapisLazulis"] += amount;

            ressourceCounters[GetRessourceTypeNumber(ressourceType)].text = ressourceType + ": " + ressources[ressourceType];
            ressourceCounters[Constants.LapisLazulis].text = "LL: " + ressources["LapisLazulis"];
        }


    }

    //format: ressourceType:amount **amount is 1 if no string after :**
    public bool SellRessourceWithoutRefund(string ressourceType) {
        string[] split = ressourceType.Split(':');
        ressourceType = split[0];
        int amount = 1;
        if (split.Length > 1)
            amount = int.Parse(split[1]);

        if (ressources[ressourceType] - amount >= 0) {
            ressources[ressourceType] -= amount;
            if (ressourceType != "LapisLazulis")
                ressourceCounters[GetRessourceTypeNumber(ressourceType)].text = ressourceType + ": " + ressources[ressourceType];
            else
                ressourceCounters[GetRessourceTypeNumber(ressourceType)].text = "LL: " + ressources[ressourceType];
            return true;
        }

        return false;
    }

    //format: ressourceType:amount **amount is 1 if no string after :**
    public void AddRessource(string type) {
        string[] split = type.Split(':');
        type = split[0];
        int amount = 1;
        if (split.Length > 1)
            amount = int.Parse(split[1]);

        if (ressources.ContainsKey(type)) {
            ressources[type] += amount;
            if(type != "LapisLazulis")
                ressourceCounters[GetRessourceTypeNumber(type)].text = type + ": " + ressources[type];
            else
                ressourceCounters[GetRessourceTypeNumber(type)].text = "LL: " + ressources[type];
        }
    }

    //format: ressourceBoughtWith:amount,ressourceBought:amount **amount is 1 if no string after :**
    public void BuyWithLL(string deal) {
        string[] split = deal.Split(',');
        if (split.Length != 2)
            return;
        string ressourceBoughtWith = split[0];
        string ressourceBought = split[1];

        if(SellRessourceWithoutRefund(ressourceBoughtWith))
            AddRessource(ressourceBought);

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

    public void OpenBuyResPanel() {
        BuyResPanel.SetActive(true);
    }
    public void CloseBuyResPanel() {
        BuyResPanel.SetActive(false);
    }

    public void ToggleUpgradePanel() {
        upgradePanel.SetActive(!upgradePanel.activeSelf);
        RefreshItemUpgradePanel();
    }

    public void RefreshItemUpgradePanel() {
        if (selectedItem == null) {
            Destroy(objSelectedPreview);
            objSelectedPreview = null;

            itemSelectedLabel.text = "No item selected";
            healthUpgradeBar.SetActive(false);
            dmgUpgradeBar.SetActive(false);
            fireRateUpgradeBar.SetActive(false);
        }
        else {
            TMP_Text costLabel = (healthUpgradeBar.GetComponentInChildren<Button>()).GetComponentInChildren<TMP_Text>();
            costLabel.text = selectedItem.MaxHealthUpgradeCost;

            costLabel = (dmgUpgradeBar.GetComponentInChildren<Button>()).GetComponentInChildren<TMP_Text>();
            costLabel.text = selectedItem.AttackDmgUpgradeCost;

            costLabel = (fireRateUpgradeBar.GetComponentInChildren<Button>()).GetComponentInChildren<TMP_Text>();
            costLabel.text = selectedItem.FireRateUpgradeCost;

            healthUpgradeBar.GetComponent<UpgradeBar>().SetLevel(selectedItem.MaxHealthLevel);
            dmgUpgradeBar.GetComponent<UpgradeBar>().SetLevel(selectedItem.AttackDmgLevel);
            fireRateUpgradeBar.GetComponent<UpgradeBar>().SetLevel(selectedItem.FireRateLevel);


            itemSelectedLabel.text = ("Item Upgrade: " + selectedItem.gameObject.name).Replace("(Clone)", "");

            healthUpgradeBar.SetActive(true);
            dmgUpgradeBar.SetActive(true);
            fireRateUpgradeBar.SetActive(true);

            if (!selectedItem.canAttack) {
                dmgUpgradeBar.SetActive(false);
                fireRateUpgradeBar.SetActive(false);
            }
        }
    }


    public void UpgradeItemMaxHealth() {
        if (selectedItem != null) {
            selectedItem.UpgradeMaxHealth(healthUpgradeBar.GetComponent<UpgradeBar>());
        }
    }

    public void UpgradeAttackDmg() {
        if (selectedItem != null) {
            selectedItem.UpgradeAttackDmg(dmgUpgradeBar.GetComponent<UpgradeBar>());
        }
    }

    public void UpgradeFireRate() {
        if (selectedItem != null) {
            selectedItem.UpgradeFireRate(fireRateUpgradeBar.GetComponent<UpgradeBar>());
        }
    }



    //--------------------------------------------------------------------------------------------//
    //
    //                      PLACING/CONSTUCTION CODE HERE....
    //
    //--------------------------------------------------------------------------------------------//
    //--------------------------------------------------------------------------------------------//



    void Start() {
        FindLocationsOfTiles();
        if (objToPlacePreview != null) {
            objToPlacePreview = Instantiate(objToPlacePreview);
            objToPlacePreview.SetActive(true);
        }

        BuyResPanel.SetActive(false);
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
        //if not over UI
        if (!EventSystem.current.IsPointerOverGameObject()) {
            Camera cam = Camera.main;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Input.GetMouseButtonDown(0) && objToPlace != null) {

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Terrain"))) {

                    Vector3 destination = new Vector3(hit.point.x, tileMap.transform.position.y, hit.point.z);
                    Vector3Int gridPos = tileMap.WorldToCell(destination);
                    SpawnObj(gridPos);
                }

            }

            if (objToPlacePreview != null) {

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Terrain"))) {

                    Vector3 destination = new Vector3(hit.point.x, tileMap.transform.position.y, hit.point.z);
                    Vector3Int gridPos = tileMap.WorldToCell(destination);

                    objToPlacePreview.transform.position = tileMap.CellToWorld(gridPos);
                }
            }
            else {
                if (Input.GetMouseButtonDown(0)) {
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Construction"))) {

                        Constructable constructionSelected = hit.collider.gameObject.GetComponent<Constructable>();

                        Destroy(objSelectedPreview);
                        objSelectedPreview = null;

                        if (constructionSelected != null && constructionSelected.itemPreview != null) {
                            objSelectedPreview = Instantiate(constructionSelected.itemPreview);

                            objSelectedPreview.SetActive(true);
                            objSelectedPreview.transform.position = constructionSelected.gameObject.transform.position;

                            selectedItem = constructionSelected;
                        }

                    }
                    else {
                        selectedItem = null;

                        Destroy(objSelectedPreview);
                        objSelectedPreview = null;
                    }
                    RefreshItemUpgradePanel();
                }
            }

            if (Input.GetKey(KeyCode.X)) {

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Construction"))) {

                    Constructable itemDestroyed = hit.collider.gameObject.GetComponent<Constructable>();
                    if (itemDestroyed != null) {
                        //refund the cost
                        AddRessource(itemDestroyed.ressourceType + ":" + itemDestroyed.cost);

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
    }

    public void SetObjectToPlace(Constructable buyableItem) {
        if (objToPlacePreview != null)
            Destroy(objToPlacePreview.gameObject);

        objToPlacePreview = Instantiate(buyableItem.itemPreview);
        objToPlacePreview.SetActive(true);

        objToPlace = buyableItem.itemModel;

        actualItemToPlace = buyableItem;
    }

    public void ClearObjectToPlace() {
        if (objToPlacePreview != null)
            Destroy(objToPlacePreview.gameObject);
        objToPlacePreview = null;
        objToPlace = null;
    }



    private void SpawnObj(Vector3Int pos) {

        if (availablePlaces.Contains(pos))
            return;

        Vector3 realPos = tileMap.CellToWorld(pos);

        if (Buy(actualItemToPlace)) {
            DataFile.stats["nbBuild"]++;
            Constructable constructable = Instantiate(objToPlace, realPos, Quaternion.identity).gameObject.GetComponent<Constructable>();
            availablePlaces.Add(pos);
            //constructable.billboard.cam = healthBarsCam.transform;
            //for (int i = 0; i < navMeshSurfaces.Length; i++)
            //{
            //    navMeshSurfaces[i].BuildNavMesh();
            //}
        }

        

    }

    public void DestroyTileObject(GameObject toDestroy) {
        Vector3 worldPos = toDestroy.transform.position;
        Vector3Int gridPos = tileMap.WorldToCell(worldPos);
        if (!availablePlaces.Contains(gridPos))
            return;

        //Remove object form unavaible place
        availablePlaces.Remove(gridPos);

        //Destroy object
        Destroy(toDestroy);
    }


    public void BuyMoreRam() {
        Application.OpenURL("https://www.youtube.com/watch?v=1Ug_KCkn-JI");
    }
}
