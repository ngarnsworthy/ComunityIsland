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
    [NonSerialized] private AssetBundle buildingsPrivate;
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
        SaveAsBinary.WriteToBinaryFile<World>(Application.persistentDataPath+"/"+name, this);
        Debug.Log(Application.persistentDataPath + "/" + name);
    }
}
