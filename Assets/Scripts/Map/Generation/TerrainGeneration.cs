using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TerrainGeneration
{
    public enum GenerationType
    {
        Perlin,
        Fractal,
        Simplex
    }

    private GenerationType generationType;
    private int chunkSize;
    private static FastNoise noise = new FastNoise((int)(UnityEngine.Random.value*int.MaxValue));
    public TerrainGeneration(int chunkSize, GenerationType generationType)
    {
        if (chunkSize <= 0)
        {
            throw new ArgumentOutOfRangeException("Chunk size less then zero");
        }

        this.chunkSize = chunkSize;
        this.generationType = generationType;
    }

    public float[,] GetData(Vector2Int location)
    {
        float[,] data = new float[chunkSize, chunkSize];
        for (int x = 0; x < chunkSize; x++)
        {
            for(int y = 0; y < chunkSize; y++)
            {
                switch(generationType)
                {
                    case GenerationType.Perlin:
                        data[x,y]=noise.GetPerlin(((float)x)/chunkSize+location.x, ((float)y)/chunkSize+location.y); break;
                    case GenerationType.Fractal:
                        data[x,y]=noise.GetPerlinFractal(((float)x) / chunkSize + location.x, ((float)y) / chunkSize + location.y); break;
                    case GenerationType.Simplex:
                        data[x,y]=noise.GetSimplex(((float)x) / chunkSize + location.x, ((float)y) / chunkSize + location.y); break;
                }
            }
        }
        return data;
    }
}
