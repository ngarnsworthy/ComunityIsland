using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World
{
    public int loadingDistance;
    Dictionary<int, Dictionary<int, Chunk>> chunks = new Dictionary<int, Dictionary<int, Chunk>>();

    public World()
    {
        chunks.Add(0, new Dictionary<int, Chunk>());
        chunks[0].Add(0, new Chunk());
    }

    public void CreateChunks(Vector3 playerLocation)
    {
        Vector2Int playerChunk = new Vector2Int((int)(playerLocation.x / 16), (int)(playerLocation.y / 16));
        for (int x = playerChunk.x-loadingDistance; x < playerChunk.x+loadingDistance; x++)
        {
            if (chunks[x] == null)
            {
                chunks.Add(x, new Dictionary<int, Chunk>());
            }
            for (int y = playerChunk.y-loadingDistance; y < playerChunk.y+loadingDistance; y++)
            {
                if (chunks[x][y] == null)
                {
                    chunks[x].Add(y, new Chunk(chunks[x][y + 1], chunks[x + 1][y], chunks[x][y - 1], chunks[x - 1][y]));
                }
            }
        }
    }
}
