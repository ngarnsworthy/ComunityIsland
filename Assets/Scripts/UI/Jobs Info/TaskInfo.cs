using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskInfo : MonoBehaviour
{
    public bool current = false;
    public CitizenTask task;
    public TextMeshProUGUI buildingsToVisit;
    public TextMeshProUGUI taskType;
    public TopBarResizer resizer;

    public void Reload()
    {
        taskType.text = "Task type: " + task.Name;
        buildingsToVisit.text = "Buildings: ";
        foreach (PlacedBuilding building in task.buildingsToVisit)
        {
            buildingsToVisit.text += building.buildingType + " " + building.location.ToString() + ", ";
        }
        buildingsToVisit.text = buildingsToVisit.text.Substring(0, buildingsToVisit.text.Length-2);
        resizer.Recalculate();
    }
}
