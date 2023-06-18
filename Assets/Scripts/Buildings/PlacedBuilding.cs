using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlacedBuilding
{
    [NonSerialized] public GameObject gameObject;
    public List<Citizen> workers;
    public List<ItemStack> items;
    public Queue<CitizenTask> tasks;
    [NonSerialized] private Building buildingPrivate;
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
    World world;

    public PlacedBuilding(Building building, SerializableVector2Int location, float height, World world)
    {
        this.world = world;
        this.building = building;
        this.location = location;
        this.height = height;
        workers = new List<Citizen>();
        tasks = new Queue<CitizenTask>();
    }
}