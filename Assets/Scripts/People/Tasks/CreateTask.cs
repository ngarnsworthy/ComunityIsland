using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CreateTask : CitizenTask
{
    public ItemStack createdItem;

    public CreateTask(PlacedBuilding building, ItemStack createdItem) : base(building)
    {
        this.createdItem = createdItem;
    }

    public override string Name => "Create resources out of nothing";

    public override bool priority => false;
}
