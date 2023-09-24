using Aoiti.Pathfinding;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
                if (TerrainGen.world.GetWalkable(new SerializableVector2Int(pos.x + x, pos.y + y)) || true)
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
    [HideInInspector] public Dictionary<PlacedBuilding, List<ItemStack>> neededItems = new Dictionary<PlacedBuilding, List<ItemStack>>();
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
        foreach (CitizenRecord citizen in records)
        {
            //Show citizen if in loaded chunks.
            citizen.gameObject.SetActive(TerrainGen.terrainGen.meshGenerater.loadedChunks.Contains(TerrainGen.world.GetChunkAtPoint(citizen.gameObject.transform.position)));


            if (citizen.employment == null)
                continue;

            if (citizen.task == null)
            {
                if (citizen.path == null)
                    citizen.path = new List<Vector3>();
                citizen.path.Clear();
                if (citizen.employment.building.levels[citizen.employment.level].createWorkers > citizen.employment.creatingWorkers.Count)
                {
                    citizen.task = new CreateTask(citizen, citizen.employment, new ItemStack(citizen.employment.building.levels[citizen.employment.level].createdItem, (int)citizen.employment.building.levels[citizen.employment.level].itemsPerSecond));
                    citizen.employment.creatingWorkers.Add(citizen);
                }
                else if (citizen.employment.building.levels[citizen.employment.level].craftingWorkers > citizen.employment.craftingWorkers.Count)
                {
                    citizen.task = new CraftingTask(citizen.employment.building.levels[citizen.employment.level].output, citizen.employment.building.levels[citizen.employment.level].craftingIngredients, citizen.employment.building.levels[citizen.employment.level].craftingSpeed, citizen.employment);
                    citizen.employment.craftingWorkers.Add(citizen);
                }
                else if (!neededItems.Values.All(i => i.Count == 0))
                {
                    foreach (KeyValuePair<PlacedBuilding, List<ItemStack>> item in neededItems)
                    {
                        if (item.Value.Count != 0)
                        {
                            Dictionary<PlacedBuilding, int> foundItems;
                            citizen.task = new MoveTask(item.Key, citizen, item.Value[0], out foundItems);
                            int foundItemCount = foundItems.Values.Sum();
                            if (foundItemCount == item.Value[0].stackSize)
                            {
                                item.Value.RemoveAt(0);
                            }
                            else
                            {
                                item.Value[0].stackSize -= foundItemCount;
                            }
                            break;
                        }
                    }
                }
                else
                {
                    continue;
                }
            }

            if (!citizen.task.firstPathGenerated)
            {
                GeneratePath(citizen);
                citizen.gameObject.SetActive(true);
                citizen.task.firstPathGenerated = true;
            }

            if (citizen.path != null)
            {
                citizen.Log();
                if (citizen.path.Count > 1 && citizen.currentPointIndex < citizen.path.Count - 1)
                {
                    citizen.gameObject.SetActive(true);
                    //On path
                    citizen.segmentPrecentMoved += (citizen.movementSpeed * Time.deltaTime) / citizen.trackLegnth / Vector3.Distance(citizen.path[citizen.currentPointIndex], citizen.path[citizen.currentPointIndex + 1]);

                    while (citizen.segmentPrecentMoved >= 1)
                    {
                        citizen.segmentPrecentMoved -= 1;
                        citizen.currentPointIndex++;
                    }

                    if (citizen.currentPointIndex < citizen.path.Count - 1)
                        citizen.gameObject.transform.position = Vector3.Lerp(citizen.path[citizen.currentPointIndex], citizen.path[citizen.currentPointIndex + 1], citizen.segmentPrecentMoved);
                }
                else
                {
                    citizen.task.Update();
                    //Path done
                    if (citizen.task.workAtBuilding)
                    {
                        //Task is not done and needs to wait before going to next building
                        citizen.gameObject.SetActive(false);
                    }
                    else
                    {
                        //Not last building
                        GeneratePath(citizen);
                    }
                }
            }
        }
    }

    public void GeneratePath(CitizenRecord record)
    {
        record.segmentPrecentMoved = 0;
        record.currentPointIndex = 0;
        record.trackLegnth = 0;
        List<SerializableVector2Int> newPath;
        pathfinder.GenerateAstarPath(new SerializableVector2Int((int)record.gameObject.transform.position.x, (int)record.gameObject.transform.position.z), record.nextBuilding.location + record.nextBuilding.building.doorLocation, out newPath);

        record.path.Clear();
        for (int i = 0; i < newPath.Count; i++)
        {
            record.path.Add(new Vector3(newPath[i].x, TerrainGen.world[newPath[i]] + heightOffset, newPath[i].y));
            if (i != 0)
            {
                record.trackLegnth += Vector3.Distance(record.path[i - 1], record.path[i]);
            }
        }
    }
}
