using System;
using UnityEngine;

[Serializable]
public class CreateTask : CitizenTask
{
    public ItemStack createdItem;

    public CreateTask(CitizenRecord citizen, PlacedBuilding building, ItemStack createdItem) : base(building, citizen)
    {
        this.createdItem = createdItem;
    }

    public override string Name => "Createing " + createdItem.item.name;

    public override bool priority => false;

    public override bool workAtBuilding => true;

    public override void Update()
    {
        if (citizen.employment.items.Contains(createdItem))
        {
            citizen.employment.items.Find((value) => { return value.Equals(createdItem); }).stackSize += createdItem.stackSize * Time.deltaTime;
        }
        else
        {
            citizen.employment.items.Add(new ItemStack(createdItem));
        }
    }
}
