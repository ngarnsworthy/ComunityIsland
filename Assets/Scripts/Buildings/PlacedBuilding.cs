using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlacedBuilding
{
    [NonSerialized] public GameObject gameObject;
    List<CitizenAI> citizenAIList;
    [NonSerialized]public List<Citizen> workers;
    public List<ItemStack> items;
    public Queue<CitizenTask> tasks;
    [NonSerialized] private Building buildingPrivate;
    [NonSerialized] PlacedBuildingComponent placedBuildingComponent;
    public Building building
    {
        get
        {
            if (buildingPrivate == null)
            {
                buildingPrivate = world.buildings.LoadAsset<Building>(buildingName);
            }
            return buildingPrivate;
        }
        set
        {
            buildingPrivate = value;
            buildingName = value.name;
        }
    }

    public string buildingName;
    public SerializableVector2Int location;
    public int level;
    public float height;
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

    public PlacedBuilding(Building building, SerializableVector2Int location, float height)
    {
        this.building = building;
        this.location = location;
        this.height = height;
        workers = new List<Citizen>();
        tasks = new Queue<CitizenTask>();
        citizenAIList = new List<CitizenAI>();
    }

    public void Load()
    {
        placedBuildingComponent = gameObject.GetComponent<PlacedBuildingComponent>();
        foreach (var item in citizenAIList)
        {
            GameObject.Instantiate(TerrainGen.terrainGen.citizenPrefab).GetComponent<Citizen>().AI = item;
            item.employment = this;
        }
    }

    public void Save()
    {
        citizenAIList.Clear();
        foreach (var item in workers)
        {
            citizenAIList.Add(item.AI);
        }
    }
}