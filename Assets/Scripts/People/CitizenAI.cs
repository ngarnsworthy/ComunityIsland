using System;
using System.Collections.Generic;

[Serializable]
public class CitizenAI
{
    [NonSerialized] public Citizen citizen;
    public CitizenTask currentTask;
    [NonSerialized] public PlacedBuilding employment;
    [NonSerialized] public PlacedBuilding nextBuilding;

    public CitizenAI(Citizen citizen)
    {
        TerrainGen.world.citizens.Add(this);
        this.citizen = citizen;
        UpdateBuilding();
        if (employment == null)
        {
            TerrainGen.world.unemployedCitizenAIs.Add(this);
        }
    }

    public void Update()
    {
        if (employment == null)
        {
            return;
        }
        if (currentTask != null && !currentTask.started)
        {
            nextBuilding = currentTask.StartTaskLocation(citizen);
            if (citizen.pathfinder == null)
            {
                citizen.pathfinder = new Pathfinder.AStarPath();
            }
            citizen.pathfinder.ClearPath();
            citizen.pathfinder.start = citizen.transform.position;
            citizen.pathfinder.end = nextBuilding.gameObject.transform.position;
        }
    }

    public bool UpdateBuilding()
    {
        Queue<Chunk> chunkQueue = new Queue<Chunk>();
        chunkQueue.Enqueue(TerrainGen.world.GetChunkAtPoint(citizen.gameObject.transform.position));
        List<Chunk> checkedChunks = new List<Chunk>();
        HashSet<Chunk> allChunks = new HashSet<Chunk>();
        while (employment == null && chunkQueue.Count != 0)
        {
            Chunk chunk = chunkQueue.Dequeue();
            if (allChunks.Count <= 100)
            {
                if (!allChunks.Contains(chunk.north) && chunk.north != null)
                {
                    chunkQueue.Enqueue(chunk.north);
                    allChunks.Add(chunk.north);
                }
                if (!allChunks.Contains(chunk.east) && chunk.east != null)
                {
                    chunkQueue.Enqueue(chunk.east);
                    allChunks.Add(chunk.east);
                }
                if (!allChunks.Contains(chunk.south) && chunk.south != null)
                {
                    chunkQueue.Enqueue(chunk.south);
                    allChunks.Add(chunk.south);
                }
                if (!allChunks.Contains(chunk.west) && chunk.west != null)
                {
                    chunkQueue.Enqueue(chunk.west);
                    allChunks.Add(chunk.west);
                }
            }

            foreach (PlacedBuilding building in chunk.placedBuildings)
            {
                if (building.building.levels[building.level].maxWorkers > building.workers.Count)
                {
                    building.workers.Add(citizen);
                    building.UpdateCitizenAIList();
                    employment = building;
                }
            }
        }

        return employment != null;
    }
}
