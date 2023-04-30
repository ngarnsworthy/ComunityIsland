using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshGenerater
{
    public Mesh mesh;
    World world;
    Queue<Chunk> neededChunks = new Queue<Chunk>();
    List<Chunk> loadedChunks = new List<Chunk>();
    Transform chunkLocation;
    Transform player;
    int loadingDistance;
    TerrainGen parent;
    public MeshGenerater(World world, Transform chunkLocation, Transform player, int loadingDistance, TerrainGen parent)
    {
        mesh = new Mesh();
        this.world = world;

        this.chunkLocation = chunkLocation;
        this.player = player;
        this.loadingDistance = loadingDistance;
        this.parent = parent;
    }

    public IEnumerator AddToQueue()
    {
        while (true)
        {
            List<Chunk> chunks = world.CreateChunks(player.position);
            foreach (var item in chunks)
            {
                if (!loadedChunks.Contains(item))
                {
                    neededChunks.Enqueue(item);
                }
            }
            yield return null;
        }
    }

    public IEnumerator ClearQueue()
    {
        while (true)
        {
            if (neededChunks.Count!=0) {
                Chunk chunk = neededChunks.Dequeue();
                parent.MakeChunk(chunk);
                loadedChunks.Add(chunk);
            }
            yield return null;
        }
    }
}
