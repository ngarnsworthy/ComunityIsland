using UnityEngine;

public class MakeRequest : MonoBehaviour
{
    public Item item;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                PlacedBuilding building = hit.collider.gameObject.GetComponent<PlacedBuilding>();
                if (building != null)
                {
                    building.tasks.Enqueue(new MoveTask(building, item, 10));
                }
            }
        }
    }
}
