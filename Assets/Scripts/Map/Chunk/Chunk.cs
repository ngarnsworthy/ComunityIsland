using System;
using UnityEngine;

public class Chunk
{
    public Chunk(int chunkSize, Vector2Int location)
    {
        if(chunkSize <= 0)
        {
            throw new ArgumentOutOfRangeException("Chunk Size");
        }

        if(location == null)
        {
            throw new ArgumentNullException("Location");
        }

        data = new float[chunkSize,chunkSize];

        this.location = location;
    }

    public Chunk(int chunkSize, Vector2Int location, TerrainGeneration terrainGeneration) : this(chunkSize, location)
    {
        data = terrainGeneration.GetData(location);
    }

    public Vector2Int location;
    public float[,] data {  get; private set; }
}
