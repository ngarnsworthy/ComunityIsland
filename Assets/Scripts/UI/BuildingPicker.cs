using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BuildingPicker : MonoBehaviour
{
    public InputActionReference changeSelection;

    public PlaceBuilding buildingPlacer;

    public Transform location;
    public GameObject buildingPickerSlot;
    public GameObject firstBuildingPickerSlot;
    List<GameObject> buildingPickerSlots = new List<GameObject>();
    List<Building> buildings = new List<Building>();

    int buildingPickerSlotIndex = 0;

    void Start()
    {
        changeSelection.action.Enable();
        buildingPickerSlots.Add(firstBuildingPickerSlot);
        string[] buildingAssets = TerrainGen.world.buildings.GetAllAssetNames();
        for (int i = 0; i < buildingAssets.Length; i++)
        {
            if (i != 0)
            {
                GameObject newGameObject = Instantiate(buildingPickerSlot, location);

                Vector3 position = newGameObject.transform.position;
                position.x = 78 * i;
                position.y = 0;
                newGameObject.transform.position = position;

                buildingPickerSlots.Add(newGameObject);
            }

            Transform child = buildingPickerSlots[i].transform.GetChild(0);

            buildings.Add(TerrainGen.world.buildings.LoadAsset<Building>(buildingAssets[i]));
            child.GetComponent<Image>().sprite = TerrainGen.world.buildings.LoadAsset<Building>(buildingAssets[i]).sprite;
        }
    }

    private void Update()
    {
        buildingPickerSlotIndex += (int)changeSelection.action.ReadValue<float>();

        if (buildingPickerSlotIndex < 0)
        {
            buildingPickerSlotIndex = Mathf.Abs(3 - (buildingPickerSlotIndex % buildingPickerSlots.Count - 1));
        }

        if (buildingPickerSlotIndex > buildingPickerSlots.Count - 1)
        {
            buildingPickerSlotIndex %= buildingPickerSlots.Count - 1;
        }

        buildingPlacer.placedBuilding = buildings[buildingPickerSlotIndex];
    }
}
