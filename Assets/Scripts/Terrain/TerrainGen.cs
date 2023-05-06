using OpenCover.Framework.Model;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class TerrainGen : MonoBehaviour
{
    public string worldName;
    public Transform player;
    World world;
    MeshGenerater meshGenerater;
    MeshFilter meshFilter;
    public Transform chunkLocation;
    public GameObject chunkGameObject;
    public int loadingDistance;
    public float scale;

    // Start is called before the first frame update
    void Start()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + "/" + worldName))
        {
            world = SaveAsBinary.ReadFromBinaryFile<World>(Application.persistentDataPath + "/" + worldName);
        }
        else
        {
            world = new World(loadingDistance, scale, Random.seed);
            world.name = worldName;
        }
        meshGenerater = new MeshGenerater(world, chunkLocation, player, loadingDistance, this);
        world.loadingDistance = loadingDistance;
        meshFilter = GetComponent<MeshFilter>();
        StartCoroutine(meshGenerater.AddToQueue());
        StartCoroutine(meshGenerater.ClearQueue());
    }

    public GameObject MakeChunk(Chunk chunk)
    {
        GameObject gameObject = Instantiate(chunkGameObject, chunkLocation);
        Mesh mesh = chunk.GenerateMesh();
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        ChunkControler chunkControler = gameObject.GetComponent<ChunkControler>();
        chunkControler.location = chunk.worldLocation;
        chunkControler.world = world;
        chunkControler.player = player;
        gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
        gameObject.name = "Chunk "+chunk.worldLocation.ToString();
        chunk.gameObject = gameObject;
        return gameObject;
    }

    private void OnDestroy()
    {
        world.Save();
    }
}
