using NUnit.Framework;
using System;
using UnityEngine;

public class ChunkRendererTests
{
    [Test]
    public void TestMeshGeneration()
    {
        for (int x = 2; x < 10; x++)
        {
            for (int y = 2; y < 10; y++)
            {
                float[,] testData = new float[x, y];

                for (int z = 0; z < x; z++)
                {
                    for (int w = 0; w < y; w++)
                    {
                        testData[z, w] = UnityEngine.Random.Range(-1, 1);
                    }
                }

                Mesh mesh = ChunkRenderer.GetMesh(testData);

                Assert.AreEqual(x * y, mesh.vertices.Length);
                Assert.AreEqual((x - 1) * (y - 1) * 6, mesh.triangles.Length);
            }
        }
    }

    [Test]
    public void TestInputValidation()
    {
        Assert.That(() => ChunkRenderer.GetMesh(null),
                  Throws.TypeOf<ArgumentNullException>());

        ChunkRenderer.GetMesh(new float[100,100]);
    }
}
