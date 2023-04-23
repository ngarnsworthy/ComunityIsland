using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGen : MonoBehaviour
{
    public Transform player;
    public int seed;
    World world;
    // Start is called before the first frame update
    void Start()
    {
        NoiseS3D.seed = seed;
        world = new World();
        StartCoroutine(LoadChunks());
    }

    IEnumerator LoadChunks()
    {
        while (true)
        {
            yield return null;
            world.CreateChunks(player.position);
        }
    }
}
