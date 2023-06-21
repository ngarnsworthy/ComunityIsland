using System;
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

[System.Serializable]
public class Chunk
{
    World world
    {
        get
        {
            if (pWorld == null)
            {
                pWorld = TerrainGen.world;
            }
            return pWorld;
        }
    }
    [NonSerialized] private World pWorld;
    public float[,] points;
    public bool[,] walkable;
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

    public Chunk(Vector2Int location, float scale)
    {
        placedBuildings = new List<PlacedBuilding>();
        worldLocation = location;

        walkable = new bool[16, 16];
        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 16; y++)
            {
                walkable[x, y] = true;
            }
        }

        points = new float[17, 17];
        for (int x = 0; x < 17; x++)
        {
            for (int y = 0; y < 17; y++)
            {
                points[x, y] = (float)NoiseS3D.Noise((x + worldLocation.x * 16) * scale, (y + worldLocation.y * 16) * scale);
            }
        }
    }

    public Vector3[] GetPoints()
    {
        Vector3[] returnPoints = new Vector3[17 * 17];
        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 16; y++)
            {
                returnPoints[x * 17 + y] = new Vector3(worldLocation.x * 16 + x, points[x, y], worldLocation.y * 16 + y);
            }
        }
        if (north == null)
        {
            world.CreateChunk(new Vector2Int(worldLocation.x, worldLocation.y + 1));
        }
        for (int x = 0; x < 17; x++)
        {
            returnPoints[x * 17 + 16] = new Vector3(worldLocation.x * 16 + x, north.points[x, 0], worldLocation.y * 16 + 16);
        }

        if (east == null)
        {
            world.CreateChunk(new Vector2Int(worldLocation.x + 1, worldLocation.y));
        }
        for (int y = 0; y < 16; y++)
        {
            returnPoints[17 * 16 + y] = new Vector3(worldLocation.x * 16 + 17, east.points[0, y], worldLocation.y * 16 + y);
        }

        if (east == null || east.north == null) 
        {
            world.CreateChunk(new Vector2Int(worldLocation.x + 1, worldLocation.y + 1));
        }

        returnPoints[17 * 16 + 16] = new Vector3(worldLocation.x * 16 + 17, east.north.points[0, 0], worldLocation.y * 16 + 17);


        return returnPoints;
    }

    public Mesh GenerateMesh()
    {
        List<int> tris = new List<int>();
        List<Vector3> points = new List<Vector3>();
        List<Vector2> UVs = new List<Vector2>();
        int start = points.Count;
        points.AddRange(this.GetPoints());

        for (int x = 0; x < 17; x++)
        {
            for (int y = 0; y < 17; y++)
            {
                UVs.Add(new Vector2(((float)x) / 17, ((float)y) / 17));
            }
        }

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
        mesh.uv = UVs.ToArray();
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

    public void UpdateChunk()
    {
        if (gameObject == null)
        {
            return;
        }
        Mesh mesh = GenerateMesh();
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
    }

    public void AddBuilding(Building building, Vector2Int location)
    {
        float height = 0;

        for (int x = (int)(location.x - (building.footprint.x / 2)); x < (int)location.x + (building.footprint.x / 2); x++)
        {
            for (int y = (int)(location.y - (building.footprint.y / 2)); y < (int)location.y + (building.footprint.y / 2); y++)
            {
                SerializableVector2Int setLocation = new SerializableVector2Int(x, y);
                if (!world.GetWalkable(setLocation))
                {
                    return;
                }
                height += world[setLocation];
            }
        }

        height /= (building.footprint.y * building.footprint.x);

        PlacedBuilding newBuilding = new PlacedBuilding(building, location, height);
        placedBuildings.Add(newBuilding);
        for (int x = (int)(location.x - (building.footprint.x / 2)); x < (int)location.x + (building.footprint.x / 2); x++)
        {
            for (int y = (int)(location.y - (building.footprint.y / 2)); y < (int)location.y + (building.footprint.y / 2); y++)
            {
                SerializableVector2Int setLocation = new SerializableVector2Int(x, y);
                world[setLocation] = height;
                world.SetWalkable(setLocation, false);
            }
        }
        if (gameObject != null)
        {
            PlaceBuilding(newBuilding);
        }
        world.BuildingBuilt();
    }

    public void PlaceBuilding(PlacedBuilding building)
    {
        GameObject gameObject = GameObject.Instantiate(world.terrainGen.buildingPrefab, this.gameObject.transform);

        gameObject.GetComponent<MeshFilter>().mesh = building.building.levels[building.level].mesh;

        gameObject.transform.position = new Vector3(building.location.x, building.height, building.location.y);

        building.Load();
    }

    public void Save()
    {
        foreach (var item in placedBuildings)
        {
            item.Save();
        }
    }
}