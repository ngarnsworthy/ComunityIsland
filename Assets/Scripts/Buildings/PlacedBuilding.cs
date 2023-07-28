using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlacedBuilding
{
    [NonSerialized] public GameObject gameObject;
    [HideInInspector]
    [NonSerialized]public List<CitizenRecord> workers;
    public List<ItemStack> items = new List<ItemStack>();
    public List<ItemStack> usedItems;
    public Queue<CitizenTask> tasks;
    public List<CitizenTask> workingTasks
    {
        get
        {
            List<CitizenTask> tasks = new List<CitizenTask>(this.tasks.ToArray());

            foreach (CitizenRecord record in workers)
            {
                if (record.task != null)
                {
                    tasks.Add(record.task);
                }
            }

            return tasks;
        }
    }
    [NonSerialized] private Building buildingPrivate;
    [NonSerialized] PlacedBuildingComponent placedBuildingComponent;
    public Building building
    {
        get
        {
            if (buildingPrivate == null)
            {
                buildingPrivate = TerrainGen.world.AssetBundle.LoadAsset<Building>(buildingType);
            }
            return buildingPrivate;
        }
        set
        {
            buildingPrivate = value;
            buildingType = value.name;
        }
    }

    public string buildingType;
    public SerializableVector2Int location;
    public int level
    {
        get { return _level; }
        set { _level = value; CitizenController.Instance.UpdateBuildings(this); }
    }
    int _level;
    public float height;

    public PlacedBuilding(Building building, SerializableVector2Int location, float height)
    {
        this.building = building;
        this.location = location;
        this.height = height;
        workers = new List<CitizenRecord>();
        tasks = new Queue<CitizenTask>();
    }

    public void Load()
    {
        placedBuildingComponent = gameObject.GetComponent<PlacedBuildingComponent>();
        placedBuildingComponent.placedBuilding = this;
        //foreach (var item in citizenRecords)
        //{
        //    GameObject.Instantiate(TerrainGen.terrainGen.citizenPrefab).GetComponent<Citizen>().AI = item;
        //    item.employment = this;
        //}
        CitizenController.Instance.LoadRecords(workers);
    }

    //public void UpdateCitizenAIList()
    //{
    //    citizenAIList.Clear();
    //    foreach (var item in workers)
    //    {
    //        citizenAIList.Add(item.AI);
    //    }
    //}

    public void Update()
    {
        //foreach (CitizenAI citizen in citizenAIList)
        //{
        //    if (citizen.currentTask == null && building.levels[level].createsItems)
        //    {
        //        citizen.currentTask = new CreateTask(this, new ItemStack(building.levels[level].createdItem, (int)building.levels[level].itemsPerSecond));
        //    }
        //    if (citizen.currentTask is CreateTask task)
        //    {
        //        if (items.Contains(task.createdItem))
        //        {
        //            items.Find((value) => value.Equals(task.createdItem)).stackSize += task.createdItem.stackSize;
        //        }
        //        else
        //        {
        //            items.Add(task.createdItem);
        //        }
        //    }
        //}
    }
}