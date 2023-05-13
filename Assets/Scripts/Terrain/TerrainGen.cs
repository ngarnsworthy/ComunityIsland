using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class TerrainGen : MonoBehaviour
{
    public AssetBundle buildings;
    public GameObject buildingPrefab;
    public bool loadSave = true;
    public string worldName;
    public Transform player;
    [HideInInspector]
    public World world;
    MeshGenerater meshGenerater;
    MeshFilter meshFilter;
    public Transform chunkLocation;
    public GameObject chunkGameObject;
    public int loadingDistance;
    public float scale;

    // Start is called before the first frame update
    void Start()
    {
        if (loadSave && System.IO.File.Exists(Application.persistentDataPath + "/" + worldName))
        {
            world = SaveAsBinary.ReadFromBinaryFile<World>(Application.persistentDataPath + "/" + worldName);
            NoiseS3D.seed = world.seed;
        }
        else
        {
            world = new World(loadingDistance, scale, Random.seed);
            world.name = worldName;
        }
        world.terrainGen = this;
        meshGenerater = new MeshGenerater(world, chunkLocation, player, loadingDistance, this);
        meshFilter = GetComponent<MeshFilter>();
        StartCoroutine(meshGenerater.AddToQueue());
        StartCoroutine(meshGenerater.ClearQueue());
    }

    private void OnDestroy()
    {
        world.Save();
    }
}
