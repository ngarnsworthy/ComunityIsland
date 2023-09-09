using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTask : CitizenTask
{

    public ItemStack output;
    public ItemStack[] inputs;
    public float craftingTime;

    IEnumerator Craft()
    {
        yield return new WaitForSeconds(craftingTime);
        foreach (var item in inputs)
        {
            building.items.Remove(item);
        }
    }

    public CraftingTask(ItemStack output, ItemStack[] inputs, float craftingTimelay, PlacedBuilding building) : base(building)
    {
        this.output = output;
        this.inputs = inputs;
        this.craftingTime = craftingTime;
    }

    public override string Name => "Crafting "+ output.item.name;

    public override bool last => true;

    public override bool done => false;

    public override bool priority => false;
}
