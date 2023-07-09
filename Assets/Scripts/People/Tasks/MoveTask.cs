using System.Collections.Generic;
using UnityEngine;

public class MoveTask : CitizenTask
{
    public override string Name
    {
        get { return "Moving items to a building"; }
    }

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

    public override PlacedBuilding StartTaskLocation(Citizen citizen)
    {
        started = true;
        List<PlacedBuilding> excludedBuildings = new List<PlacedBuilding>();
        excludedBuildings.Add(building);
        buildingsToVisit = ItemLocator.LocateItem(citizen.transform.position, itemToGet, count, excludedBuildings);
        float minDistance = float.PositiveInfinity;
        PlacedBuilding closestBuilding = null;
        foreach (PlacedBuilding building in buildingsToVisit)
        {
            float distance = Vector3.Distance(building.gameObject.transform.position, citizen.transform.position);
            if (distance < minDistance)
            {
                closestBuilding = building;
                minDistance = distance;
            }
        }
        buildingsToVisit.Remove(closestBuilding);
        return closestBuilding;
    }

    public override PlacedBuilding NextTaskLocation(Citizen citizen)
    {
        ItemStack itemStack = new ItemStack(itemToGet, count);
        if (lastBuilding == building)
        {
            if (building.items.Contains(itemStack))
            {
                building.items.Find((value) => { return value == itemStack; }).stackSize+=count;
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
            float distance = Vector3.Distance(building.gameObject.transform.position, citizen.transform.position);
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
