using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobsInfo : MonoBehaviour
{
    public CitizenAI citizen;
    public GameObject taskPrefab;

    public float Reload()
    {
        float y = 0;

        TaskInfo currentTaskInfo = Instantiate(taskPrefab, transform).GetComponent<TaskInfo>();
        currentTaskInfo.transform.position = new Vector3(0, -y, 0);
        currentTaskInfo.task = citizen.currentTask;
        currentTaskInfo.current = true;
        currentTaskInfo.Reload();
        y += currentTaskInfo.GetComponent<RectTransform>().rect.height;

        foreach (CitizenTask task in citizen.tasks)
        {
            TaskInfo taskInfo = Instantiate(taskPrefab, transform).GetComponent<TaskInfo>();
            currentTaskInfo.transform.position = new Vector3(0, -y, 0);
            currentTaskInfo.task = citizen.currentTask;
            currentTaskInfo.Reload();
            y += currentTaskInfo.GetComponent<RectTransform>().rect.height;
        }

        return y;
    }
}
