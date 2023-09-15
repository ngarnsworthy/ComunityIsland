using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveTask : CitizenTask
{
    public override string Name
    {
        get { return "Moving items to a building"; }
    }

    public override bool last => _last;
    bool _last = false;


    public Dictionary<PlacedBuilding, int> buildingData;
    ItemStack itemToGet;
    int count;
    public bool finished = false;
    public MoveTask(PlacedBuilding building, CitizenRecord citizen, ItemStack itemToGet, out Dictionary<PlacedBuilding, int> buildingData) : base(building, citizen)
    {
        this.itemToGet = itemToGet;
        List<PlacedBuilding> excludedBuildings = new List<PlacedBuilding> { building };
        this.buildingData = ItemLocator.LocateItem(citizen.gameObject.transform.position, itemToGet, excludedBuildings);
        buildingData = this.buildingData;
        itemToGet.stackSize = buildingData.Values.Sum();
    }

    public override PlacedBuilding NextTaskLocation()
    {
        if (finished)
        {
            ItemStack foundItemStack = building.items.Find(i => i.Equals(itemToGet));
            if (foundItemStack == null)
            {
                building.items.Add(itemToGet);
            }
            else
            {
                foundItemStack.stackSize += count;
            }
            return building;
        }
        else if (buildingData.Count == 0)
        {
            finished = true;
            return building;
        }
        else
        {
            float minDistance = float.PositiveInfinity;
            PlacedBuilding closestBuilding = null;
            PlacedBuilding lastBuilding = null;
            foreach (var building in buildingData.Keys)
            {
                float distance = Vector3.Distance(building.gameObject.transform.position, citizen.gameObject.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    lastBuilding = building;
                    closestBuilding = lastBuilding;
                }
            }

            ItemStack lastBuildingItemStack = lastBuilding.reservedItems.Find(i => i.Equals(itemToGet));
            if (lastBuildingItemStack.stackSize == buildingData[lastBuilding])
            {
                lastBuilding.reservedItems.Remove(lastBuildingItemStack);
            }
            else
            {
                lastBuildingItemStack.stackSize -= buildingData[lastBuilding];
            }

            buildingData.Remove(lastBuilding);
            return closestBuilding;
        }
    }
}
