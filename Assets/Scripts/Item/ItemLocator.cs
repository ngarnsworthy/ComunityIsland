using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public static class ItemLocator
{
    public static List<PlacedBuilding> LocateItem(Vector3 location, Item item, int count, List<PlacedBuilding> excludedBuildings = null)
    {
        List<PlacedBuilding> buildings = new List<PlacedBuilding>();
        int foundItems = 0;
        Queue<Chunk> chunkQueue = new Queue<Chunk>();
        chunkQueue.Enqueue(TerrainGen.terrainGen.world.GetChunkAtPoint(location));
        List<Chunk> checkedChunks = new List<Chunk>();
        HashSet<Chunk> allChunks = new HashSet<Chunk>();
        while (foundItems < count)
        {
            Chunk chunk = chunkQueue.Dequeue();
            if (allChunks.Count <= 1000)
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
                if (excludedBuildings!=null&&excludedBuildings.Contains(building))
                    continue;
                ItemStack foundItemStack = building.items.Find((e) => { return e.item = item; });
                if (foundItemStack!=null)
                {
                    buildings.Add(building);
                    foundItems += foundItemStack.stackSize;
                }
            }
        }
        return buildings;
    }
}
