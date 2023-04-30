using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerater
{
    public Mesh mesh;
    World world;
    public List<Chunk> loadedChunks = new List<Chunk>();
    public MeshGenerater(World world)
    {
        mesh = new Mesh();
        this.world = world;
    }

    bool Changed(List<Chunk> newChunks)
    {
        if (loadedChunks.SequenceEqual(newChunks))
        {
            return false;
        }
        else
        {
            loadedChunks = newChunks;
            return true;
        }
    }

    public Mesh GenerateMesh(List<Chunk> newChunks)
    {
        if (Changed(newChunks))
        {
            List<int> tris = new List<int>();
            List<Vector3> points = new List<Vector3>();
            foreach (var chunk in loadedChunks)
            {
                int start = points.Count;
                points.AddRange(chunk.GetPoints());
                for (int x = 0; x < 16; x++)
                {
                    for (int y = 0; y < 16; y++)
                    {
                        int tile = x * 16 + y+start;
                        if (x == 15 && y == 15)
                        {

                        }
                        else if (x == 15)
                        {

                        }
                        else if (y == 15)
                        {

                        }
                        else
                        {
                            int[] newPoints = { tile, tile + 1, tile + 17, tile, tile + 17, tile + 16 };
                            tris.AddRange(newPoints);
                        }
                    }
                }
            }
            mesh.vertices = points.ToArray();
            mesh.triangles = tris.ToArray();
            return mesh;
        }
        else
        {
            return null;
        }
    }
}
