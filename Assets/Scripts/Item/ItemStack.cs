using System;
public class ItemStack
{
    public Item item;
    public int stackSize;

    public ItemStack(Item item, int stackSize)
    {
        this.item = item;
        this.stackSize = stackSize;
    }
}
