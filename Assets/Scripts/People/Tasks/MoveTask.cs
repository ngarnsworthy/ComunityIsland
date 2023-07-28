using System.Collections.Generic;
using UnityEngine;

public class MoveTask : CitizenTask
{
    public override string Name
    {
        get { return "Moving items to a building"; }
    }

    public override bool last => buildingsToVisit.Count == 0;

    public PlacedBuilding lastBuilding;
    Item itemToGet;
    int count;
    int remaining;
    public MoveTask(PlacedBuilding building, Item itemToGet, int count) : base(building)
    {
        this.itemToGet = itemToGet;
        this.count = count;
        remaining = count;
    }

    public override PlacedBuilding NextTaskLocation(CitizenRecord citizen)
    {
        if (!started)
        {
            List<PlacedBuilding> excludedBuildings = new List<PlacedBuilding>
            {
                building
            };
            buildingsToVisit = ItemLocator.LocateItem(citizen.gameObject.transform.position, itemToGet, count, excludedBuildings);
            started = true;
        }
        ItemStack itemStack = new ItemStack(itemToGet, count);
        if (lastBuilding == building)
        {
            if (building.items.Contains(itemStack))
            {
                building.items.Find((value) => { return value == itemStack; }).stackSize += count;
            }
            else
            {
                building.items.Add(itemStack);
            }
            Done = true;
        }
        if (buildingsToVisit.Count == 0)
        {
            lastBuilding = building;
            return building;
        }
        itemStack = lastBuilding.usedItems.Find((value) => { return value == itemStack; });
        itemStack.stackSize -= remaining;
        if (itemStack.stackSize <= 0)
        {
            lastBuilding.usedItems.Remove(itemStack);
        }

        float minDistance = float.PositiveInfinity;
        PlacedBuilding closestBuilding = null;
        foreach (PlacedBuilding building in buildingsToVisit)
        {
            float distance = Vector3.Distance(building.gameObject.transform.position, citizen.gameObject.transform.position);
            if (distance < minDistance)
            {
                closestBuilding = building;
                minDistance = distance;
            }
        }
        lastBuilding = closestBuilding;
        buildingsToVisit.Remove(closestBuilding);
        return closestBuilding;
    }
}
