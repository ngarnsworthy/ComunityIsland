using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobsInfoPage : MonoBehaviour
{
    public EditBuilding editBuilding;
    public GameObject jobsInfoPrefab;
    public GameObject workerInfoPrefab;

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
        foreach (Citizen citizen in editBuilding.selectedBuilding.workers)
        {
            if (citizen.AI.currentTask != null)
            {
                WorkerInfo workerInfo = Instantiate(workerInfoPrefab, transform).GetComponent<WorkerInfo>();
                Vector3 position = workerInfo.transform.position;
                position.y -= y;
                y += spaceing;
                workerInfo.transform.position = position;
                workerInfo.citizen = citizen.AI;
                workerInfo.Reload();

                JobsInfo info = Instantiate(jobsInfoPrefab, transform).GetComponent<JobsInfo>();
                position = info.transform.position;
                position.y -= y;
                y += spaceing;
                info.transform.position = position;
                info.citizen = citizen.AI;
                y += info.Reload();
            }
        }
    }
}
