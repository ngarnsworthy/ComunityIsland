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

    public abstract bool last
    {
        get;
    }
    public abstract bool done
    {
        get;
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

    public virtual PlacedBuilding NextTaskLocation(CitizenRecord citizen) { started = true; return building; }
}
