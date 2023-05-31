using UnityEngine;
using System.Collections;

public abstract class CitizenTask
{
    public Citizen citizen;
    public PlacedBuilding building;

    public CitizenTask(Citizen citizen, PlacedBuilding building)
    {
        this.citizen = citizen;
        this.building = building;
    }

    public abstract PlacedBuilding StartTaskLocation();
    public abstract PlacedBuilding NextTaskLocation();
}
