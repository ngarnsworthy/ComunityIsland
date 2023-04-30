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
    World world;
    public float[,] points;
    public Vector2Int worldLocation;
    public Chunk north
    {
        get
        {
            //Location of the chunk
            Vector2Int chunkLocation = new Vector2Int(worldLocation.x, worldLocation.y+1);
            //Check if the chunk exists
            if (world.chunks.ContainsKey(chunkLocation))
            {
                return world.chunks[chunkLocation];
            }
            else
            {
                return null;
            }
        }
    }
    public Chunk east
    {
        get
        {
            //Location of the chunk
            Vector2Int chunkLocation = new Vector2Int(worldLocation.x + 1, worldLocation.y);
            //Check if the chunk exists
            if (world.chunks.ContainsKey(chunkLocation))
            {
                return world.chunks[chunkLocation];
            }
            else
            {
                return null;
            }
        }
    }
    public Chunk south
    {
        get
        {
            //Location of the chunk
            Vector2Int chunkLocation = new Vector2Int(worldLocation.x, worldLocation.y-1);
            //Check if the chunk exists
            if (world.chunks.ContainsKey(chunkLocation))
            {
                return world.chunks[chunkLocation];
            }
            else
            {
                return null;
            }
        }
    }
    public Chunk west
    {
        get
        {
            //Location of the chunk
            Vector2Int chunkLocation = new Vector2Int(worldLocation.x - 1, worldLocation.y);
            //Check if the chunk exists
            if (world.chunks.ContainsKey(chunkLocation))
            {
                return world.chunks[chunkLocation];
            }
            else
            {
                return null;
            }
        }
    }

    public Chunk(Vector2Int location, World world)
    {
        this.world = world;
        worldLocation = location;

        points = new float[16,16];
        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 16; y++)
            {
                points[x,y] = (float)NoiseS3D.Noise(x,y);
            }
        }
    }

    public Vector3[] GetPoints()
    {
        Vector3[] returnPoints = new Vector3[16 * 16];
        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 16; y++)
            {
                returnPoints[x * 16 + y] = new Vector3(worldLocation.x*16+x,points[x,y],worldLocation.y*16+y);
            }
        }
        return returnPoints;
    }
}
