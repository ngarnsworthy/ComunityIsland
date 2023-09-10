using System.Collections.Generic;
using UnityEngine;

public static class ItemLocator
{
    public static Dictionary<PlacedBuilding, int> LocateItem(Vector3 location, ItemStack item, List<PlacedBuilding> excludedBuildings = null)
    {
        ItemStack remainingItemStack = new ItemStack(item);
        Dictionary<PlacedBuilding, int> buildings = new Dictionary<PlacedBuilding, int>();
        Queue<Chunk> chunkQueue = new Queue<Chunk>();
        chunkQueue.Enqueue(TerrainGen.world.GetChunkAtPoint(location));
        List<Chunk> checkedChunks = new List<Chunk>();
        HashSet<Chunk> allChunks = new HashSet<Chunk>();
        while (remainingItemStack.stackSize != 0)
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
                if (excludedBuildings != null && excludedBuildings.Contains(building))
                    continue;
                if (building.items.Find(i => i.Equals(item)) is ItemStack foundItemStack)
                {
                    int foundItemCount = building.ReserveIfPosible(remainingItemStack);
                    remainingItemStack.stackSize -= foundItemCount;
                    buildings.Add(building, foundItemCount);
                }
            }
        }
        return buildings;
    }
}
