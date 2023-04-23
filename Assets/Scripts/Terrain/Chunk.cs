using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum ChunkType
//{
//    water,
//    forest,
//    mountain,
//    mountainTop,
//    beach,
//    plains,
//}

public class Chunk
{
    public float[][] points;
    public Vector2Int worldLocation;
    public Chunk north;
    public Chunk east;
    public Chunk south;
    public Chunk west;

    public Chunk(Chunk north = null, Chunk east = null, Chunk south = null, Chunk west = null)
    {
        this.north = north;
        this.east = east;
        this.south = south;
        this.west = west;
        if (north!=null)
        {
            north.south = this;
        }
        if (east != null)
        {
            east.west = this;
        }
        if (south != null)
        {
            south.north = this;
        }
        if (west != null)
        {
            west.east = this;
        }
        Vector2[] locations = new Vector2[16*16];
        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 16; y++)
            {
                locations[x * 16 + y] = new Vector2(16 * worldLocation.x + x, 16 * worldLocation.y + y);
            }
        }
        float[] newPoints = NoiseS3D.NoiseArrayGPU(locations);
        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 16; y++)
            {
                points[x][y] = newPoints[x * 16 + y];
            }
        }
    }
}
