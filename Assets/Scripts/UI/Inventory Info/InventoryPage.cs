using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    public EditBuilding editBuilding;
    public GameObject itemPrefab;

    private void OnEnable()
    {
        if (editBuilding.selectedBuilding.items == null)
            return;
        float y = 0;
        foreach(ItemStack item in editBuilding.selectedBuilding.items)
        {
            ItemScript itemScript = Instantiate(itemPrefab).GetComponent<ItemScript>();
            Vector3 position = itemScript.transform.position;
            position.y -= y;
            y += itemScript.GetComponent<RectTransform>().rect.height;
            itemScript.transform.position = position;

            itemScript.item = item;
            itemScript.Reload();
        }    
    }
}
