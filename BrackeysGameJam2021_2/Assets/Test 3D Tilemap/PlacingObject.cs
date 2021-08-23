using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class PlacingObject : MonoBehaviour
{
    private GameObject objToPlace;
    private GameObject objPreview;

    [SerializeField] private Tilemap tileMap;
    [SerializeField] private List<Vector3> availablePlaces;

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

                SpawnWall(gridPos);

            }

        }

        if(objPreview != null) {
            Camera cam = Camera.main;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Terrain"))) {

                Vector3 destination = new Vector3(hit.point.x, tileMap.transform.position.y, hit.point.z);

                Vector3Int gridPos = tileMap.WorldToCell(destination);
                if (availablePlaces.Contains(gridPos))
                    objPreview.GetComponentInChildren<Renderer>().material.SetColor("error placing", new Color32(255, 0, 0, 40));
                else
                    objPreview.GetComponentInChildren<Renderer>().material.SetColor("error placing", new Color32(176, 176, 176, 40));


                objPreview.transform.position = tileMap.CellToWorld(gridPos);

            }
        }
    }

    public void SetObjectToPlace(GameObject toPlace, GameObject preview) {
        if (objPreview != null)
            Destroy(objPreview.gameObject);

        objPreview = Instantiate(preview);

        objToPlace = toPlace;
    }

    public void ClearObjectToPlace() {
        if (objPreview != null)
            Destroy(objPreview.gameObject);
        objPreview = null;
        objToPlace = null;
    }



    private void SpawnWall(Vector3Int pos) {

        if (availablePlaces.Contains(pos))
            return;

        Vector3 realPos = tileMap.CellToWorld(pos);

        Instantiate(objToPlace, realPos, Quaternion.identity);


        availablePlaces.Add(pos);

    }
}
