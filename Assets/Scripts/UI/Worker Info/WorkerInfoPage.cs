using UnityEngine;

public class WorkerInfoPage : MonoBehaviour
{
    public EditBuilding editBuilding;
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
            WorkerInfo info = Instantiate(workerInfoPrefab, transform).GetComponent<WorkerInfo>();
            Vector3 position = info.transform.position;
            position.y -= y;
            y += spaceing;
            info.transform.position = position;
            info.citizen = citizen.AI;
            info.Reload();
        }
    }
}
