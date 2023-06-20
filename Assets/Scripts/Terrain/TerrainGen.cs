using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class TerrainGen : MonoBehaviour
{
    public static TerrainGen terrainGen;
    public static World world;
    public AssetBundle buildings;
    public GameObject buildingPrefab;
    public bool loadSave = true;
    public string worldName;
    public Transform player;
    [HideInInspector]
    MeshGenerater meshGenerater;
    MeshFilter meshFilter;
    public Transform chunkLocation;
    public GameObject chunkGameObject;
    public int loadingDistance;
    public float scale;
    public GameObject citizenPrefab;

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

        TerrainGen.terrainGen = this;
    }

    private void OnDestroy()
    {
        world.Save();
    }
}
