using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceBuilding : MonoBehaviour
{
    public Camera camera;
    TerrainGen terrainGen;
    public Building placedBuilding;
    public InputActionReference placeBuilding;

    private void Start()
    {
        placeBuilding.action.Enable();
        terrainGen = GameObject.Find("Terrain").GetComponent<TerrainGen>();
    }

    void Update()
    {
        if (placeBuilding.action.ReadValue<float>() != 0)
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

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
