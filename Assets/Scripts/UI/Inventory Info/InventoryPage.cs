using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    public EditBuilding editBuilding;
    public GameObject itemPrefab;
    public float spacing;

    private void OnEnable()
    {
        if (editBuilding.selectedBuilding.items == null)
            return;
        float y = 0;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        foreach (ItemStack item in editBuilding.selectedBuilding.items)
        {
            ItemScript itemScript = Instantiate(itemPrefab, transform).GetComponent<ItemScript>();
            Vector3 position = itemScript.transform.position;
            position.y -= y;
            y += spacing;
            itemScript.transform.position = position;

            itemScript.item = item;
            itemScript.Reload();
        }
    }

    private void Update()
    {
        if (Time.frameCount % 10 == 0)
        {
            foreach (Transform child in transform)
            {
                ItemScript itemScript = child.GetComponent<ItemScript>();
                itemScript.Reload();
            }
        }
    }
}
