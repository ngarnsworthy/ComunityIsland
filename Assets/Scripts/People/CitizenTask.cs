using System;
using System.Collections.Generic;

[Serializable]
public abstract class CitizenTask
{
    public abstract string Name
    {
        get;
    }
    public List<PlacedBuilding> buildingsToVisit = new List<PlacedBuilding>();
    public PlacedBuilding building;
    public bool started = false;

    public CitizenTask(PlacedBuilding building)
    {
        this.building = building;
    }

    public abstract PlacedBuilding StartTaskLocation(Citizen citizen);
    public abstract PlacedBuilding NextTaskLocation(Citizen citizen);
}
