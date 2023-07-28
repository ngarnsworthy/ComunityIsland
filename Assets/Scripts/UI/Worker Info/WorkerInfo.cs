using TMPro;
using UnityEngine;

public class WorkerInfo : MonoBehaviour
{
    public CitizenRecord citizen;
    public TextMeshProUGUI taskName;
    public TextMeshProUGUI currentTarget;
    public TopBarResizer resizer;

    public void Reload()
    {
        if (citizen.task != null)
        {
            taskName.text = "Current task: " + citizen.task.Name;
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
