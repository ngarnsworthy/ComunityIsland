using System;

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

    public abstract bool workAtBuilding
    {
        get;
    }
    public virtual bool priority
    {
        get { return true; }
    }
    public PlacedBuilding building;
    protected CitizenRecord citizen;
    public bool firstPathGenerated = false;

    public CitizenTask(PlacedBuilding building, CitizenRecord citizen)
    {
        this.building = building;
        this.citizen = citizen;
    }

    protected CitizenTask(PlacedBuilding building)
    {
        this.building = building;
    }

    public virtual PlacedBuilding NextTaskLocation() { return building; }

    public virtual void Update() { }
}
