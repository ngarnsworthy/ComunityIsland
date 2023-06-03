using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static Chunk;

[System.Serializable]
public class World
{
    public AssetBundle buildings
    {
        get
        {
            if (!buildingsPrivate)
            {
                buildingsPrivate = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "buildings"));
            }
            return buildingsPrivate;
        }
    }
    public AssetBundle items
    {
        get
        {
            if (!itemsPrivate)
            {
                itemsPrivate = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "items"));
            }
            return itemsPrivate;
        }
    }
    [NonSerialized] private AssetBundle buildingsPrivate;
    [NonSerialized] private AssetBundle itemsPrivate;
    [NonSerialized] public TerrainGen terrainGen; //Callback
    public int seed;
    public string name = "World";
    public float scale;
    public int loadingDistance;
    public Dictionary<SerializableVector2Int, Chunk> chunks = new Dictionary<SerializableVector2Int, Chunk>();

    public World(int loadingDistance, float scale, int seed)
    {
        NoiseS3D.seed = seed;
        chunks.Add(new Vector2Int(0, 0), new Chunk(new Vector2Int(0, 0), this, scale));
        this.loadingDistance = loadingDistance;
        this.scale = scale;
        this.seed = seed;
    }

    public Chunk GetChunkAtPoint(Vector3 location)
    {
        if (chunks.ContainsKey(new Vector2Int((int)(location.x / 16), (int)(location.z / 16))))
        {
            return chunks[new Vector2Int((int)(location.x / 16), (int)(location.z / 16))];
        }
        return null;
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

    public void Save()
    {
        SaveAsBinary.WriteToBinaryFile<World>(Application.persistentDataPath + "/" + name, this);
        Debug.Log(Application.persistentDataPath + "/" + name);
    }

    public class ChunkLocation
    {
        public Chunk chunk;
        public int x;
        public int y;

        public ChunkLocation(Chunk chunk, int x, int y)
        {
            this.chunk = chunk;
            this.x = x;
            this.y = y;
        }
    }

    public static ChunkLocation ChunkLocationFromPoint(Vector3 point)
    {
        return new ChunkLocation(TerrainGen.world.chunks[new Vector2Int((int)(point.x / 16), (int)(point.z / 16))], (int)(point.x % 16), (int)(point.z % 16));
    }

    public static ChunkLocation ChunkLocationFromPoint(Vector2Int point)
    {
        return new ChunkLocation(TerrainGen.world.chunks[new Vector2Int((int)(point.x / 16), (int)(point.y / 16))], (int)(point.x % 16), (int)(point.y % 16));
    }

    public Vector3 Vector3FromChunkLocation(ChunkLocation point)
    {
        return new Vector3(point.x, ChunkLocationToHeight(point), point.y);
    }

    public float ChunkLocationToHeight(ChunkLocation point)
    {
        return point.chunk.points[point.x, point.y];
    }
}
