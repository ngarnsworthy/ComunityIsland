using System.Collections.Generic;

public class CitizenAI
{
    public Citizen citizen;
    public Queue<CitizenTask> tasks;
    public CitizenTask currentTask;
    public PlacedBuilding employment;
    PlacedBuilding nextBuilding;

    public void Update()
    {
        if(employment == null)
        {
            Queue<Chunk> chunkQueue = new Queue<Chunk>();
            chunkQueue.Enqueue(TerrainGen.world.GetChunkAtPoint(citizen.gameObject.transform.position));
            List<Chunk> checkedChunks = new List<Chunk>();
            HashSet<Chunk> allChunks = new HashSet<Chunk>();
            while (employment == null && chunkQueue.Count!=0)
            {
                Chunk chunk = chunkQueue.Dequeue();
                if (allChunks.Count <= 100)
                {
                    if (!allChunks.Contains(chunk.north))
                    {
                        chunkQueue.Enqueue(chunk.north);
                        allChunks.Add(chunk.north);
                    }
                    if (!allChunks.Contains(chunk.east))
                    {
                        chunkQueue.Enqueue(chunk.east);
                        allChunks.Add(chunk.east);
                    }
                    if (!allChunks.Contains(chunk.south))
                    {
                        chunkQueue.Enqueue(chunk.south);
                        allChunks.Add(chunk.south);
                    }
                    if (!allChunks.Contains(chunk.west))
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
                        employment = building;
                    }
                }
            }

            return;
        }
        if (tasks == null)
        {
            tasks.Enqueue(employment.tasks.Dequeue());
        }
        if (currentTask == null)
        {
            currentTask = tasks.Dequeue();
        }
        if (!currentTask.started)
        {
            nextBuilding = currentTask.StartTaskLocation(citizen);
            citizen.pathfinder.ClearPath();
            citizen.pathfinder.start=citizen.transform.position;
            citizen.pathfinder.end=nextBuilding.gameObject.transform.position;
        }
    }
}
