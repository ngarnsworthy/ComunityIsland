using Aoiti.Pathfinding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static UnityEditor.Progress;

public class CitizenController : MonoBehaviour
{
    public GameObject citizenGameObject;
    public static CitizenController Instance { get; private set; }

    Pathfinder<SerializableVector2Int> pathfinder = new Pathfinder<SerializableVector2Int>((p1, p2) => { return Vector2Int.Distance(p1, p2); }, (SerializableVector2Int pos) =>
    {
        Dictionary<SerializableVector2Int, float> neighbours = new Dictionary<SerializableVector2Int, float>();
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {

                if (x == 0 && y == 0) continue;
                SerializableVector2Int dir = new SerializableVector2Int(x, y);
                World.ChunkLocation chunkLocation = new World.ChunkLocation(TerrainGen.world.chunks[new SerializableVector2Int(0, 0)], x + pos.x, y + pos.y);
                if (TerrainGen.world.GetWalkable(new SerializableVector2Int(pos.x + x, pos.y + y))) ;
                {
                    neighbours.Add(new SerializableVector2Int(x + pos.x, y + pos.y), Vector3.Distance(TerrainGen.world.Vector3FromChunkLocation(chunkLocation), new Vector3(pos.x, TerrainGen.world[new SerializableVector2Int(pos.x, pos.y)], pos.y)));
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
            if (record.path != null && record.path.Count > 1 && record.currentPointIndex < record.path.Count - 2)
            {
                record.segmentPrecentMoved += record.movementSpeed / record.trackLegnth / Vector3.Distance(record.path[record.currentPointIndex], record.path[record.currentPointIndex + 1]);

                while (record.segmentPrecentMoved >= 1)
                {
                    record.segmentPrecentMoved--;
                    record.currentPointIndex++;
                }

                if (record.currentPointIndex >= record.path.Count && !record.task.last)
                {
                    GeneratePath(record);
                }
                else
                {
                    record.gameObject.transform.position = Vector3.Lerp(record.path[record.currentPointIndex], record.path[record.currentPointIndex + 1], record.segmentPrecentMoved);
                    Debug.Log(Vector3.Lerp(record.path[record.currentPointIndex], record.path[record.currentPointIndex + 1], record.segmentPrecentMoved));
                }
            }
            if (record.task == null && record.employment.building.levels[record.employment.level].createsItems)
            {
                record.task = new CreateTask(record.employment, new ItemStack(record.employment.building.levels[record.employment.level].createdItem, (int)record.employment.building.levels[record.employment.level].itemsPerSecond));
            }
            if (!record.task.started)
            {
                GeneratePath(record);
            }
            if(record.task is CreateTask)
            {

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
            record.path.Add(new Vector3(newPath[i].x, TerrainGen.world[newPath[i]], newPath[i].y));
            if (i != 0)
            {
                record.trackLegnth += Vector3.Distance(record.path[i - 1], record.path[i]);
            }
        }
    }
}
