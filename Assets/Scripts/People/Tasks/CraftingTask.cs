using System.Collections;
using System.Collections.Generic;
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

    public override bool workAtBuilding => true;

    public override bool priority => false;

    public override void Update()
    {
        if (!crafting)
        {
            CraftingTask task = (CraftingTask)citizen.task;
            bool hasAllItems = true;
            foreach (var item in task.inputs)
            {
                if (!citizen.employment.items.Contains(item) || citizen.employment.items.Find(i => i.Equals(item)).stackSize <= item.stackSize)
                {
                    hasAllItems = false;
                    if (!itemRequested)
                    {
                        itemRequested = true;
                        if (!CitizenController.Instance.neededItems.ContainsKey(citizen.employment))
                            CitizenController.Instance.neededItems.Add(citizen.employment, new List<ItemStack>());
                        if (CitizenController.Instance.neededItems[citizen.employment].Contains(item))
                        {
                            CitizenController.Instance.neededItems[citizen.employment].Find(i => i.Equals(item)).stackSize += item.stackSize;
                        }
                        else
                        {
                            CitizenController.Instance.neededItems[citizen.employment].Add(new ItemStack(item));
                        }
                    }
                }
            }
            if (!hasAllItems)
                return;
            CitizenController.Instance.StartCoroutine(Craft(citizen.employment));
        }
    }
}
