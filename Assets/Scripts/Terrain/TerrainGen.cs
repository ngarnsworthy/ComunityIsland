using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (MeshFilter))]
public class TerrainGen : MonoBehaviour
{
    public Transform player;
    public int seed;
    World world;
    MeshGenerater meshGenerater;
    MeshFilter meshFilter;
    // Start is called before the first frame update
    void Start()
    {
        NoiseS3D.seed = seed;
        world = new World();
        meshGenerater = new MeshGenerater(world);
        world.loadingDistance = 10;
        meshFilter = GetComponent<MeshFilter>();
        StartCoroutine(LoadChunks());
    }

    IEnumerator LoadChunks()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            Mesh newMesh = meshGenerater.GenerateMesh(world.CreateChunks(player.position));
            if (newMesh != null)
            {
                meshFilter.mesh = newMesh;
                Debug.Log("Mesh Updated");
            }
        }
    }
}
