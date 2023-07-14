using TMPro;
using UnityEngine;

public class WorkerInfo : MonoBehaviour
{
    public CitizenAI citizen;
    public TextMeshProUGUI taskName;
    public TextMeshProUGUI currentTarget;
    public TopBarResizer resizer;

    public void Reload()
    {
        if (citizen.currentTask != null)
        {
            taskName.text = "Current task: " + citizen.currentTask.Name;
            currentTarget.text = "Current Target: " + citizen.nextBuilding.buildingType + " " + citizen.nextBuilding.location.ToString();
        }
        else
        {
            taskName.text = "Current task: N/A";
            currentTarget.text = "Current Target: N/A";
        }
        resizer.Recalculate();
    }
}
