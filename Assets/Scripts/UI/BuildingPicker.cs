using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BuildingPicker : MonoBehaviour
{
    public Transform location;
    public PlaceBuilding buildingPlacer;
    public GameObject buildingPickerSlot;
    public GameObject firstBuildingPickerSlot;
    List<GameObject> buildingPickerSlots = new List<GameObject>();

    void Start()
    {
        buildingPickerSlots.Add(firstBuildingPickerSlot);
        string[] buildingAssets = TerrainGen.world.buildings.GetAllAssetNames();
        for (int i = 0; i < buildingAssets.Length - 1; i++)
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

            UnityAction buttonAction = new UnityAction(()=> { ChangeBuilding(buildingAssets[i]); });
            child.GetComponent<Button>().onClick.AddListener(buttonAction);

            child.GetComponent<Image>().sprite = TerrainGen.world.buildings.LoadAsset<Building>(buildingAssets[i]).sprite;
        }
    }

    public void ChangeBuilding(string name)
    {
        buildingPlacer.placeBuilding = TerrainGen.world.buildings.LoadAsset<Building>(name);
    }
}
