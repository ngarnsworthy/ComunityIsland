using System.Collections.Generic;
using UnityEngine;

public class World
{
    public float scale;
    public int loadingDistance;
    public Dictionary<Vector2Int, Chunk> chunks = new Dictionary<Vector2Int, Chunk>();

    public World(int loadingDistance, float scale)
    {
        chunks.Add(new Vector2Int(0, 0), new Chunk(new Vector2Int(0, 0), this, scale));
        this.loadingDistance = loadingDistance;
        this.scale = scale;
    }

    public List<Chunk> CreateChunks(Vector3 playerLocation)
    {
        List<Chunk> loadedChunks = new List<Chunk>();
        Vector2Int playerChunk = new Vector2Int((int)(playerLocation.x / 16), (int)(playerLocation.z / 16));
        for (int x = playerChunk.x - loadingDistance; x < playerChunk.x + loadingDistance; x++)
        {
            for (int y = playerChunk.y - loadingDistance; y < playerChunk.y + loadingDistance; y++)
            {
                if (!chunks.ContainsKey(new Vector2Int(x, y)))
                {
                    chunks.Add(new Vector2Int(x, y), new Chunk(new Vector2Int(x, y), this, scale));
                }
                loadedChunks.Add(chunks[new Vector2Int(x, y)]);
            }
        }
        return loadedChunks;
    }
}
