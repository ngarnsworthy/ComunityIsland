using NUnit.Framework;
using System;
using UnityEngine;

public class GenerationTest
{
    [Test]
    public void TestOutputCorrect()
    {
        foreach (TerrainGeneration.GenerationType type in (TerrainGeneration.GenerationType[])Enum.GetValues(typeof(TerrainGeneration.GenerationType)))
        {
            Debug.Log("");
            Debug.Log("Type: " + type);

            TerrainGeneration terrainGeneration = new TerrainGeneration(16, type);

            float[,] data = terrainGeneration.GetData(new Vector2Int(0, 0));
            Assert.NotNull(data);

            Assert.AreEqual(data.GetLength(0), 16);
            Assert.AreEqual(data.GetLength(1), 16);

            Debug.Log("Data:");

            for (int x = 0; x < data.GetLength(0); x++)
            {
                string row = "";

                for (int y = 0; y < data.GetLength(1); y++)
                {
                    row += data[x, y] + "|";
                }

                Debug.Log(row);
            }
        }
    }

    [Test]
    public void TestInputValidation()
    {
        Assert.That(() => new TerrainGeneration(0, TerrainGeneration.GenerationType.Perlin),
                  Throws.TypeOf<ArgumentOutOfRangeException>());

        Assert.That(() => new TerrainGeneration(-10, TerrainGeneration.GenerationType.Perlin),
          Throws.TypeOf<ArgumentOutOfRangeException>());
    }
}
