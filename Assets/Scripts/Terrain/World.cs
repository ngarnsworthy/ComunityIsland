using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class World
{
    public AssetBundle AssetBundle
    {
        get
        {
            if (!assetBundlesPrivate)
            {
                assetBundlesPrivate = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "buildings-items"));
            }
            return assetBundlesPrivate;
        }
    }

    public float this[SerializableVector2Int location]
    {
        get
        {
            try
            {
                return chunks[new SerializableVector2Int(Mathf.FloorToInt(location.x / 16), Mathf.FloorToInt(location.y / 16))].points[(location.x % 16 + 16) % 16, (location.y % 16 + 16) % 16];
            }
            catch
            {
                return 0;
            }

        }
        set
        {
            try
            {
                chunks[new SerializableVector2Int(Mathf.FloorToInt(location.x / 16), Mathf.FloorToInt(location.y / 16))].points[(location.x % 16 + 16) % 16, (location.y % 16 + 16) % 16] = value;
            }
            catch
            { }
        }
    }

    public bool GetWalkable(SerializableVector2Int location)
    {
        return chunks[new SerializableVector2Int(Mathf.FloorToInt(location.x / 16), Mathf.FloorToInt(location.y / 16))].walkable[(location.x % 16 + 16) % 16, (location.y % 16 + 16) % 16];
    }

    public void SetWalkable(SerializableVector2Int location, bool value)
    {
        chunks[new SerializableVector2Int(Mathf.FloorToInt(location.x / 16), Mathf.FloorToInt(location.y / 16))].walkable[(location.x % 16 + 16) % 16, (location.y % 16 + 16) % 16] = value;
    }

    [NonSerialized] private AssetBundle assetBundlesPrivate;
    [NonSerialized] public TerrainGen terrainGen; //Callback
    public int seed;
    public string name = "World";
    public float scale;
    public int loadingDistance;
    public Dictionary<SerializableVector2Int, Chunk> chunks;
    public List<Chunk> forceLoadedChunks;

    public World(int loadingDistance, float scale, int seed)
    {
        chunks = new Dictionary<SerializableVector2Int, Chunk>();
        NoiseS3D.seed = seed;
        chunks.Add(new Vector2Int(0, 0), new Chunk(new Vector2Int(0, 0), scale));
        forceLoadedChunks = new List<Chunk>();
        this.loadingDistance = loadingDistance;
        this.scale = scale;
        this.seed = seed;
    }

    public Chunk GetChunkAtPoint(Vector3 location)
    {
        if (!chunks.ContainsKey(new Vector2Int((int)(location.x / 16), (int)(location.z / 16))))
        {
            CreateChunks(location);
        }
        return chunks[new Vector2Int((int)(location.x / 16), (int)(location.z / 16))];
    }

    public List<Chunk> CreateChunks(Vector3 playerLocation, int loadingDistance = 0)
    {
        if (loadingDistance == 0)
        {
            loadingDistance = this.loadingDistance;
        }
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

    public Chunk CreateChunk(Vector2Int location)
    {
        Chunk chunk;
        if (!chunks.ContainsKey(location))
        {
            chunk = new Chunk(location, scale);
            chunks.Add(location, chunk);
        }
        else
        {
            chunk = chunks[location];
        }
        return chunk;
    }

    public void Save()
    {
        foreach (Chunk item in chunks.Values)
        {
            item.Save();
        }
        CitizenController.Instance.Save();
        SaveAsBinary.WriteToBinaryFile<World>(Application.persistentDataPath + "/" + name, this);
        Debug.Log(Application.persistentDataPath + "/" + name);
    }

    public class ChunkLocation
    {
        public Chunk chunk;
        public int x
        {
            get
            {
                return _x;
            }
            private set
            {
                _x = value;
                while (_x < 0)
                {
                    chunk = chunk.west;
                    _x += 16;
                }
                while (_x > 16)
                {
                    chunk = chunk.east;
                    x -= 16;
                }
            }
        }
        int _x;
        public int y
        {
            get
            {
                return _y;
            }
            private set
            {
                _y = value;
                while (_y < 0)
                {
                    chunk = chunk.south;
                    _y += 16;
                }
                while (_y > 16)
                {
                    chunk = chunk.north;
                    _y -= 16;
                }
            }
        }
        int _y;

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

    public static SerializableVector2Int SerializableVector2IntFromVector3(Vector3 point)
    {
        return new Vector2Int(((int)point.x), ((int)point.y));
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
