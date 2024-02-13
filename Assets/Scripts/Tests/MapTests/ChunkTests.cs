using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ChunkTests
{
    [Test]
    public void TestConstructor()
    {
        Chunk chunk = new Chunk(16);
    }

    [Test]
    public void TestConstructorInputValidation() 
    {
        Assert.That(() => new Chunk(0),
                  Throws.TypeOf<ArgumentOutOfRangeException>());

        Assert.That(() => new Chunk(-10),
          Throws.TypeOf<ArgumentOutOfRangeException>());

        new Chunk(10);
    }
}
