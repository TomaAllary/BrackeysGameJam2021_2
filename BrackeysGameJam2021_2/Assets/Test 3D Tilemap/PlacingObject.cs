using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class PlacingObject : MonoBehaviour
{
    [SerializeField] private GameObject wallPrefab;

    [SerializeField] private Tilemap tileMap;
    [SerializeField] private List<Vector3> availablePlaces;

    void Start() {
        FindLocationsOfTiles();
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


    private void FixedUpdate() {

        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0)) {
            Camera cam = Camera.main;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Terrain"))) {

                Vector3 destination = new Vector3(hit.point.x, tileMap.transform.position.y, hit.point.z);

                Vector3Int gridPos = tileMap.WorldToCell(destination);

                SpawnWall(gridPos);

            }

        }
    }

    private void SpawnWall(Vector3Int pos) {

        if (availablePlaces.Contains(pos))
            return;

        Vector3 realPos = tileMap.CellToWorld(pos);

        Instantiate(wallPrefab, realPos, Quaternion.identity);


        availablePlaces.Add(pos);

    }
}
