using Aoiti.Pathfinding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CitizenController : MonoBehaviour
{
    public float heightOffset;
    public GameObject citizenGameObject;
    public static CitizenController Instance { get; private set; }

    Pathfinder<SerializableVector2Int> pathfinder = new Pathfinder<SerializableVector2Int>((p1, p2) => { return Vector2Int.Distance(p1, p2); }, (SerializableVector2Int pos) =>
    {
        Vector3 startLocation = new Vector3(pos.x, TerrainGen.world[new SerializableVector2Int(pos.x, pos.y)], pos.y);
        Dictionary<SerializableVector2Int, float> neighbours = new Dictionary<SerializableVector2Int, float>();
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {

                if (x == 0 && y == 0) continue;
                if (TerrainGen.world.GetWalkable(new SerializableVector2Int(pos.x + x, pos.y + y))||true)
                {
                    neighbours.Add(new SerializableVector2Int(x + pos.x, y + pos.y), Vector3.Distance(startLocation, new Vector3(pos.x, TerrainGen.world[new SerializableVector2Int(pos.x, pos.y)], pos.y)));
                }

            }
        }
        return neighbours;
    }, 500);
    [HideInInspector] public List<CitizenRecord> records = new List<CitizenRecord>();
    [HideInInspector] public List<CitizenRecord> loadedRecords = new List<CitizenRecord>();
    [HideInInspector] public List<CitizenRecord> unemployedRecords = new List<CitizenRecord>();
    List<PlacedBuilding> notFiledBuildings = new List<PlacedBuilding>();

    int nextId = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateBuildings(PlacedBuilding building)
    {
        if (notFiledBuildings.Contains(building))
            return;
        notFiledBuildings.Add(building);
        FillBuildings();
    }

    void FillBuildings()
    {
        if (unemployedRecords.Count > 0)
        {
            foreach (var item in unemployedRecords.ToArray())
            {
                if (notFiledBuildings.Count > 0)
                {
                    notFiledBuildings[0].workers.Add(item);
                    item.employment = notFiledBuildings[0];
                    unemployedRecords.Remove(item);
                    if (notFiledBuildings[0].workers.Count >= notFiledBuildings[0].building.levels[notFiledBuildings[0].level].maxWorkers)
                    {
                        notFiledBuildings.RemoveAt(0);
                    }
                }
            }
        }
    }

    public void LoadRecords(IEnumerable<CitizenRecord> records)
    {
        foreach (CitizenRecord record in records)
        {
            if (record.id == -1)
            {
                record.id = nextId++;
            }
            if (record.employment != null)
            {
                if (!record.employment.workers.Contains(record))
                    record.employment.workers.Add(record);
            }
            if (!this.records.Contains(record))
            {
                this.records.Add(record);

                if (record.employment == null)
                {
                    unemployedRecords.Add(record);
                    if (notFiledBuildings.Count > 0)
                    {
                        UpdateBuildings(notFiledBuildings[0]);
                    }
                }
            }
            if (!loadedRecords.Contains(record))
            {
                record.gameObject = Instantiate(citizenGameObject, record.location, Quaternion.identity);
                loadedRecords.Add(record);
            }
            if (notFiledBuildings.Count > 0)
            {
                FillBuildings();
            }
        }
    }

    public void Save()
    {
        foreach (CitizenRecord record in records)
        {
            record.location = record.gameObject.transform.position;
        }
    }

    public void Load()
    {
        LoadRecords(records);
    }

    void Update()
    {
        foreach (CitizenRecord record in records)
        {
            //Show citizen if in loaded chunks.
            record.gameObject.SetActive(TerrainGen.terrainGen.meshGenerater.loadedChunks.Contains(TerrainGen.world.GetChunkAtPoint(record.gameObject.transform.position)));

            if (record.employment == null)
                continue;
            if (record.path != null && record.path.Count > 1)
            {
                if(record.currentPointIndex < record.path.Count - 1)
                {
                    //On path
                    record.segmentPrecentMoved += (record.movementSpeed * Time.deltaTime) / record.trackLegnth / Vector3.Distance(record.path[record.currentPointIndex], record.path[record.currentPointIndex + 1]);

                    while (record.segmentPrecentMoved >= 1)
                    {
                        record.segmentPrecentMoved -= 1;
                        record.currentPointIndex++;
                    }

                    if (record.currentPointIndex < record.path.Count - 1)
                        record.gameObject.transform.position = Vector3.Lerp(record.path[record.currentPointIndex], record.path[record.currentPointIndex + 1], record.segmentPrecentMoved);
                }
                else
                {
                    //Off path
                    if (record.task.last)
                    {
                        //Last Building in path
                        if(record.task.done)
                        {
                            //Task is done and ready to be replaced
                            record.task = null;
                        }
                        else
                        {
                            //Task is not done and needs to wait before going to next building
                            record.gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        //Not last building
                        GeneratePath(record);
                    }
                }
            }
            if (record.task == null && record.employment.building.levels[record.employment.level].createsItems)
            {
                record.task = new CreateTask(record.employment, new ItemStack(record.employment.building.levels[record.employment.level].createdItem, (int)record.employment.building.levels[record.employment.level].itemsPerSecond));
            }
            if (!record.task.started)
            {
                GeneratePath(record);
                record.gameObject.SetActive(true);
            }
            if (record.task is CreateTask && !record.gameObject.activeInHierarchy)
            {
                CreateTask task = (CreateTask)record.task;
                if (record.employment.items.Contains(task.createdItem))
                {
                    record.employment.items.Find((value) => { return value.Equals(task.createdItem); }).stackSize += task.createdItem.stackSize;
                }
                else
                {
                    record.employment.items.Add(task.createdItem);
                }
            }
        }
    }

    public void GeneratePath(CitizenRecord record) {
        record.segmentPrecentMoved = 0;
        record.currentPointIndex = 0;
        record.trackLegnth = 0;
        List<SerializableVector2Int> newPath;
        pathfinder.GenerateAstarPath(new SerializableVector2Int((int)record.gameObject.transform.position.x, (int)record.gameObject.transform.position.z), record.nextBuilding.location + record.nextBuilding.building.doorLocation, out newPath);

        record.path.Clear();
        for (int i = 0; i < newPath.Count; i++)
        {
            record.path.Add(new Vector3(newPath[i].x, TerrainGen.world[newPath[i]]+heightOffset, newPath[i].y));
            if (i != 0)
            {
                record.trackLegnth += Vector3.Distance(record.path[i - 1], record.path[i]);
            }
        }
    }
}
