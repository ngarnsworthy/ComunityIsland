using System;
using System.Collections.Generic;
using System.IO;
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

[System.Serializable]
public class Chunk
{
    World world;
    public float[,] points;
    public SerializableVector2Int worldLocation;
    public List<PlacedBuilding> placedBuildings;

    [NonSerialized] public GameObject gameObject;
    public Chunk north
    {
        get
        {
            //Location of the chunk
            Vector2Int chunkLocation = new Vector2Int(worldLocation.x, worldLocation.y + 1);
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
            Vector2Int chunkLocation = new Vector2Int(worldLocation.x, worldLocation.y - 1);
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

    public Chunk(Vector2Int location, World world, float scale)
    {
        placedBuildings = new List<PlacedBuilding>();
        this.world = world;
        worldLocation = location;

        points = new float[17, 17];
        for (int x = 0; x < 17; x++)
        {
            for (int y = 0; y < 17; y++)
            {
                points[x, y] = (float)NoiseS3D.Noise((x+worldLocation.x*16)*scale, (y+worldLocation.y*16)*scale);
            }
        }
    }

    public Vector3[] GetPoints()
    {
        Vector3[] returnPoints = new Vector3[17 * 17];
        for (int x = 0; x < 17; x++)
        {
            for (int y = 0; y < 17; y++)
            {
                returnPoints[x * 17 + y] = new Vector3(worldLocation.x * 16 + x, points[x, y], worldLocation.y * 16 + y);
            }
        }
        return returnPoints;
    }

    public Mesh GenerateMesh()
    {
        List<int> tris = new List<int>();
        List<Vector3> points = new List<Vector3>();
        int start = points.Count;
        points.AddRange(this.GetPoints());
        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 16; y++)
            {
                int tile = x * 17 + y + start;
                int[] newPoints = { tile, tile + 1, tile + 18, tile, tile + 18, tile + 17 };
                tris.AddRange(newPoints);
            }
        }
        Mesh mesh = new Mesh();
        mesh.vertices = points.ToArray();
        mesh.triangles = tris.ToArray();
        return mesh;
    }

    public GameObject MakeChunk(GameObject chunkGameObject, Transform chunkLocation, Transform player)
    {
        GameObject gameObject = GameObject.Instantiate(chunkGameObject, chunkLocation);
        Mesh mesh = GenerateMesh();
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        ChunkControler chunkControler = gameObject.GetComponent<ChunkControler>();
        chunkControler.location = worldLocation;
        chunkControler.world = world;
        chunkControler.player = player;
        gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
        gameObject.name = "Chunk " + worldLocation.ToString();
        this.gameObject = gameObject;
        foreach (var item in placedBuildings)
        {
            PlaceBuilding(item);
        }
        return gameObject;
    }

    public void AddBuilding(Building building, Vector2Int location, float height)
    {
        PlacedBuilding newBuilding = new PlacedBuilding(building, location, height, world);
        placedBuildings.Add(newBuilding);
        for (int x = (int)(location.x - (building.footprint.x / 2)); x < (int)location.x + (building.footprint.x / 2); x++)
        {
            for (int y = (int)(location.y - (building.footprint.y / 2)); y < (int)location.y + (building.footprint.y / 2); y++)
            {
                if ((y > 16 && x > 16) || (y<0&&x < 0))
                {
                    
                    continue;
                }
                if (x > 16 || x < 0)
                {

                    continue;
                }
                if (y > 16 || y < 0)
                {

                    continue;
                }
                points[x, y] = height;
            }
        }
        PlaceBuilding(newBuilding);
    }

    public void PlaceBuilding(PlacedBuilding building)
    {
        GameObject gameObject = GameObject.Instantiate(world.terrainGen.buildingPrefab, this.gameObject.transform);

        gameObject.GetComponent<MeshFilter>().mesh = building.building.levels[building.level].mesh;

        gameObject.transform.position = new Vector3(worldLocation.x * 16 + building.location.x,building.height , worldLocation.y * 16 + building.location.y);
    }
}