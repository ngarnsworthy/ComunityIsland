using System;
using System.Collections.Generic;

[Serializable]
public abstract class CitizenTask
{
    public abstract string Name
    {
        get;
    }
    public bool Done
    {
        get;
        protected set;
    }
    public virtual bool priority
    {
        get { return true; }
    }
    public List<PlacedBuilding> buildingsToVisit = new List<PlacedBuilding>();
    public PlacedBuilding building;
    public bool started = false;

    public CitizenTask(PlacedBuilding building)
    {
        this.building = building;
    }

    public virtual PlacedBuilding StartTaskLocation(Citizen citizen) { return building; }
    public virtual PlacedBuilding NextTaskLocation(Citizen citizen) { return building; }
}
