using System;

[Serializable]
public abstract class CitizenTask
{
    public PlacedBuilding building;
    public bool started = false;

    public CitizenTask(PlacedBuilding building)
    {
        this.building = building;
    }

    public abstract PlacedBuilding StartTaskLocation(Citizen citizen);
    public abstract PlacedBuilding NextTaskLocation(Citizen citizen);
}
