using TMPro;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public ItemStack item;
    public TextMeshProUGUI stackSize;
    public TextMeshProUGUI itemName;
    public TopBarResizer resizer;

    public void Reload()
    {
        stackSize.text = "Size: " + item.stackSize;
        itemName.text = item.item.name;

        resizer.Recalculate();
    }
}
