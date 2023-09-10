using TMPro;
using UnityEngine;

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
        buildingsToVisit.text = "";
        if(task is MoveTask moveTask)
        {
            buildingsToVisit.text = "Buildings To Visit: ";
            foreach (PlacedBuilding building in moveTask.buildingData.Keys)
            {
                buildingsToVisit.text += building.buildingType + " " + building.location.ToString() + ", ";
            }
            buildingsToVisit.text = buildingsToVisit.text.Substring(0, buildingsToVisit.text.Length - 2);
        }

        resizer.Recalculate();
    }
}
