using System;

[Serializable]
public class CreateTask : CitizenTask
{
    public ItemStack createdItem;

    public CreateTask(PlacedBuilding building, ItemStack createdItem) : base(building)
    {
        this.createdItem = createdItem;
    }

    public override string Name => "Createing " + createdItem.item.name;

    public override bool priority => false;

    public override bool last => true;
}
