using System.Collections;
using UnityEngine;

public class CraftingTask : CitizenTask
{

    public ItemStack output;
    public ItemStack[] inputs;
    public float craftingTime;
    public bool crafting;
    public bool itemRequested = false;
    public IEnumerator Craft(PlacedBuilding building)
    {
        Debug.Log("Starting Crafting");
        crafting = true;
        foreach (var item in inputs)
        {
            ItemStack foundItemStack = building.items.Find(i => i.Equals(item));
            if (foundItemStack.stackSize == item.stackSize)
            {
                building.items.Remove(item);
            }
            else
            {
                foundItemStack.stackSize -= item.stackSize;
            }
        }
        yield return new WaitForSeconds(craftingTime);
        if (building.items.Contains(output))
        {
            building.items.Find(i => i.Equals(output)).stackSize += output.stackSize;
        }
        else
        {
            building.items.Add(new ItemStack(output));
        }
        crafting = false;
        itemRequested = false;
        Debug.Log("Crafting Done");
    }

    public CraftingTask(ItemStack output, ItemStack[] inputs, float craftingTime, PlacedBuilding building) : base(building)
    {
        this.output = output;
        this.inputs = inputs;
        this.craftingTime = craftingTime;
    }

    public override string Name => "Crafting " + output.item.name;

    public override bool last => true;

    public override bool priority => false;
}
