using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveTask : CitizenTask
{
    public override string Name
    {
        get { return "Moving items to a building"; }
    }

    public override bool workAtBuilding => _last;
    bool _last = false;


    public Dictionary<PlacedBuilding, int> buildingData;
    ItemStack itemToGet;
    int count;
    KeyValuePair<PlacedBuilding, int> lastbuilding;
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
        if(lastbuilding.Key != null)
        {
            buildingData.Remove(lastbuilding.Key);
            ItemStack foundItemStack = lastbuilding.Key.reservedItems.Find(i => i.Equals(itemToGet));
            foundItemStack.stackSize -= lastbuilding.Value;

            if (foundItemStack.stackSize == 0)
            {
                lastbuilding.Key.reservedItems.Remove(foundItemStack);
            }
        }

        if (buildingData.Count > 0)
        {
            float minDistance = float.PositiveInfinity;
            KeyValuePair<PlacedBuilding, int> closestBuilding;
            foreach (KeyValuePair<PlacedBuilding, int> building in buildingData)
            {
                float distace = Vector3.Distance(building.Key.gameObject.transform.position, citizen.gameObject.transform.position);
                if (distace < minDistance)
                {
                    minDistance = distace;
                    closestBuilding = building;
                }
            }

            lastbuilding = closestBuilding;
            return closestBuilding.Key;
        }
        else
        {
            lastbuilding = new KeyValuePair<PlacedBuilding, int>(building, (int)-itemToGet.stackSize);
            return building;
        }
    }
}
