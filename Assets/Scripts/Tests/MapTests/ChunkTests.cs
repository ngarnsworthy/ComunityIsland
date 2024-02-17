using NUnit.Framework;
using System;
using UnityEngine;

public class ChunkTests
{
    [Test]
    public void TestConstructor()
    {
        int chunkSize = 16;

        Chunk chunk = new Chunk(chunkSize, new Vector2Int(0, 0));

        Assert.NotNull(chunk.data);

        Assert.AreEqual(chunkSize, chunk.data.GetLength(0));
        Assert.AreEqual(chunkSize, chunk.data.GetLength(1));

        TerrainGeneration terrainGeneration = new TerrainGeneration(chunkSize, TerrainGeneration.GenerationType.Perlin);
        chunk = new Chunk(chunkSize, new Vector2Int(0, 0), terrainGeneration);

        Assert.NotNull(chunk.data);

        Assert.AreEqual(chunkSize, chunk.data.GetLength(0));
        Assert.AreEqual(chunkSize, chunk.data.GetLength(1));

        Assert.AreEqual(chunk.data, terrainGeneration.GetData(chunk.location));
    }

    [Test]
    public void TestConstructorInputValidation()
    {
        Assert.That(() => new Chunk(0, new Vector2Int(0, 0)),
                  Throws.TypeOf<ArgumentOutOfRangeException>());

        Assert.That(() => new Chunk(-10, new Vector2Int(0, 0)),
          Throws.TypeOf<ArgumentOutOfRangeException>());

        new Chunk(10, new Vector2Int(0, 0));
    }
}
