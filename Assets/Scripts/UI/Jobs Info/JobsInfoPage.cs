using UnityEngine;

public class JobsInfoPage : MonoBehaviour
{
    public EditBuilding editBuilding;
    public GameObject taskInfoPrefab;
    public Transform workingTasks;
    public Transform neededTasks;

    public float spaceing = 50;
    private void OnEnable()
    {
        if (editBuilding.selectedBuilding.workers != null)
        {
            UpdateList();
        }
    }

    void UpdateList()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        float y = 0;
        foreach (CitizenTask task in editBuilding.selectedBuilding.workingTasks)
        {
            TaskInfo info = Instantiate(taskInfoPrefab, workingTasks).GetComponent<TaskInfo>();
            info.current = true;
            info.task = task;
            info.Reload();
        }
        foreach (CitizenTask task in editBuilding.selectedBuilding.tasks.ToArray())
        {
            TaskInfo info = Instantiate(taskInfoPrefab, neededTasks).GetComponent<TaskInfo>();
            info.task = task;
            info.Reload();
        }
    }
}
