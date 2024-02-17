using System;
using System.Collections.Generic;
using UnityEngine;

public class ChunkRenderer
{
    public static Mesh GetMesh(float[,] data)
    {
        if (data == null)
            throw new ArgumentNullException("Chunk");

        Mesh mesh = new Mesh();

        List<int> tris = new List<int>();
        List<Vector3> points = new List<Vector3>();

        Vector2Int size = new Vector2Int(data.GetLength(0), data.GetLength(1));

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                points.Add(new Vector3(x, data[x, y], y));
            }
        }

        for (int x = 0; x < size.x - 1; x++)
        {
            for (int y = 0; y < size.y - 1; y++)
            {
                tris.AddRange(GetTris(x, y, size.y));

                //Debug.Log($"X: {x} Y: {y} Data: {string.Join("|", GetTris(x, y, data.GetLength(0)))}");
            }
        }


        mesh.vertices = points.ToArray();
        mesh.SetTriangles(tris, 0);
        mesh.RecalculateNormals();

        mesh.name = "Chunk mesh";

        return mesh;
    }

    public static int[] GetTris(int x, int y, int ySize)
    {
        int startIndex = x * ySize + y;

        int[] data = { startIndex + ySize, startIndex, startIndex + ySize + 1, startIndex + ySize + 1, startIndex, startIndex + 1 };
        return data;
    }
}
