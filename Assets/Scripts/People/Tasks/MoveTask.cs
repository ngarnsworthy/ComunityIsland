using System.Collections.Generic;
using UnityEngine;

public class MoveTask : CitizenTask
{
    Item itemToGet;
    int count;
    public List<PlacedBuilding> buildingsToVisit = new List<PlacedBuilding>();
    public MoveTask(PlacedBuilding building, Item itemToGet, int count) : base(building)
    {
        this.itemToGet = itemToGet;
        this.count = count;
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
}
