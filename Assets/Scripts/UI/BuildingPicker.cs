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
    Building[] buildings;

    int buildingPickerSlotIndex = 0;

    void Start()
    {
        changeSelection.action.Enable();
        buildingPickerSlots.Add(firstBuildingPickerSlot);
        buildings = TerrainGen.world.AssetBundle.LoadAllAssets<Building>();
        for (int i = 0; i < buildings.Length; i++)
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

            child.GetComponent<Image>().sprite = buildings[i].sprite;
        }
    }

    private void Update()
    {
        buildingPickerSlotIndex += (int)Mathf.Clamp(changeSelection.action.ReadValue<float>(), -1, 1);

        if (buildingPickerSlotIndex < 0)
        {
            buildingPickerSlotIndex = Mathf.Abs(3 - (buildingPickerSlotIndex % buildingPickerSlots.Count - 1));
        }

        if (buildingPickerSlotIndex > buildingPickerSlots.Count - 1)
        {
            buildingPickerSlotIndex %= buildingPickerSlots.Count;
        }

        Debug.Log(buildingPickerSlotIndex);

        if (changeSelection.action.ReadValue<float>() != 0)
        {
            foreach (var item in buildingPickerSlots)
            {
                item.transform.GetChild(0).GetComponent<Image>().color = Color.white;
            }
            buildingPickerSlots[buildingPickerSlotIndex].transform.GetChild(0).GetComponent<Image>().color = Color.gray;
        }
        buildingPlacer.placedBuilding = buildings[buildingPickerSlotIndex];
    }
}
