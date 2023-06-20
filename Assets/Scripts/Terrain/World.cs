using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

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

    public float this[SerializableVector2Int location]
    {
        get
        {
            SerializableVector2Int cLocation = new SerializableVector2Int(Mathf.FloorToInt(location.x / 16), Mathf.FloorToInt(location.y / 16));
            if (!chunks.ContainsKey(cLocation))
            {
                return 0;
            }
            Chunk chunk = chunks[cLocation];
            if (location.x < 0 && location.y < 0)
            {
                SerializableVector2Int loc = new SerializableVector2Int(location.x % 16, location.y % 16) + 16;
                return chunk.points[loc.x, loc.y];
            }
            else if (location.x < 0)
            {
                SerializableVector2Int loc = new SerializableVector2Int(16 + (location.x % 16), location.y % 16);
                return chunk.points[loc.x, loc.y];
            }
            else if (location.y < 0)
            {
                SerializableVector2Int loc = new SerializableVector2Int(location.x % 16, 16 + (location.y % 16));
                return chunk.points[loc.x, loc.y];
            }
            else
            {
                SerializableVector2Int loc = new SerializableVector2Int(location.x % 16, location.y % 16);
                return chunk.points[loc.x, loc.y];
            }
        }
        set
        {

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
        chunks.Add(new Vector2Int(0, 0), new Chunk(new Vector2Int(0, 0), scale));
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
                    chunks.Add(new Vector2Int(x, y), new Chunk(new Vector2Int(x, y), scale));
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
        foreach (Chunk item in chunks.Values)
        {
            item.Save();
        }
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
        return new ChunkLocation(TerrainGen.world.chunks[new Vector2Int(((int)point.x / 16), ((int)point.z / 16))], ((int)point.x % 16), ((int)point.z % 16));
    }

    public static ChunkLocation ChunkLocationFromPoint(Vector2Int point)
    {
        return new ChunkLocation(TerrainGen.world.chunks[new Vector2Int(((int)point.x / 16), ((int)point.y / 16))], (int)(point.x % 16), ((int)point.y % 16));
    }

    public Vector3 Vector3FromChunkLocation(ChunkLocation point)
    {
        return new Vector3(point.x, ChunkLocationToHeight(point), point.y);
    }

    public float ChunkLocationToHeight(ChunkLocation point)
    {
        try
        {
            return point.chunk.points[point.x, point.y];
        }
        catch (Exception e)
        {
            return 0;
        }
    }
}
