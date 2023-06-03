using UnityEngine;

public class FillBuilding : MonoBehaviour
{
    public Item item;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                PlacedBuilding building = hit.collider.gameObject.GetComponent<PlacedBuilding>();
                if (building != null)
                {
                    building.items.Add(new ItemStack(item, 10));
                }
            }
        }
    }
}
