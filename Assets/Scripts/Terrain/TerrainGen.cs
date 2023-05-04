using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class TerrainGen : MonoBehaviour
{
    public Transform player;
    public int seed;
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
        NoiseS3D.seed = seed;
        world = new World(loadingDistance, scale);
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
        chunk.gameObject = gameObject;
        return gameObject;
    }
}
