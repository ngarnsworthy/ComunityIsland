using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBuilding : MonoBehaviour
{
    TerrainGen terrainGen;
    public Building placeBuilding;

    private void Start()
    {
        terrainGen = GameObject.Find("Terrain").GetComponent<TerrainGen>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 location = hit.point;
                Vector2Int rayChunk = new Vector2Int((int)(location.x / 16), (int)(location.z / 16));
                Debug.Log(rayChunk.ToString());
                Debug.Log(terrainGen.world.chunks[rayChunk]);
                Chunk chunk = terrainGen.world.chunks[rayChunk];
                chunk.AddBuilding(placeBuilding, new Vector2Int((int)location.x % 16, (int)location.z % 16), location.y);
            }
        }
    }
}
