using UnityEngine;

public class PlacedBuildingComponent : MonoBehaviour
{
    public PlacedBuilding placedBuilding;

    private void Update()
    {
        placedBuilding.Update();
    }
}
