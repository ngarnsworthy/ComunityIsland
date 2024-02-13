using System;
using UnityEngine;

public class Chunk
{
    public Chunk(int chunkSize)
    {
        if(chunkSize <= 0)
        {
            throw new ArgumentOutOfRangeException("Chunk Size");
        }

        data = new float[chunkSize,chunkSize];
    }

    public float[,] data {  get; private set; }
}
