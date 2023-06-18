using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceBuilding : MonoBehaviour
{
    TerrainGen terrainGen;
    public Building placedBuilding;
    public InputActionReference placeBuilding;

    private void Start()
    {
        terrainGen = GameObject.Find("Terrain").GetComponent<TerrainGen>();
    }

    void Update()
    {
        if (placeBuilding.action.ReadValue<bool>())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 location = hit.point;
                Vector2Int rayChunk = new Vector2Int((int)(location.x / 16), (int)(location.z / 16));
                Debug.Log(TerrainGen.world.chunks[rayChunk]);
                Chunk chunk = TerrainGen.world.chunks[rayChunk];
                chunk.AddBuilding(placedBuilding, new Vector2Int((int)location.x % 16, (int)location.z % 16), location.y);
            }
        }
    }
}
