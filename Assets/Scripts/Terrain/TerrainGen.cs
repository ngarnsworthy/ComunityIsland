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


    // Start is called before the first frame update
    void Start()
    {
        NoiseS3D.seed = seed;
        world = new World();
        meshGenerater = new MeshGenerater(world, chunkLocation, player, 5, this);
        world.loadingDistance = 10;
        meshFilter = GetComponent<MeshFilter>();
        StartCoroutine(meshGenerater.AddToQueue());
        StartCoroutine(meshGenerater.ClearQueue());
    }

    public void MakeChunk(Chunk chunk)
    {
        GameObject gameObject = Instantiate(chunkGameObject, chunkLocation);
        gameObject.GetComponent<MeshFilter>().mesh = chunk.GenerateMesh();
        gameObject.GetComponent<ChunkControler>().location = chunk.worldLocation;
    }
}
