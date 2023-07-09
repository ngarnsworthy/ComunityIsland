using System.Collections.Generic;

[System.Serializable]
public class ItemStack
{
    public Item item;
    public int stackSize;

    public ItemStack(Item item, int stackSize)
    {
        this.item = item;
        this.stackSize = stackSize;
    }

    public override bool Equals(object obj)
    {
        return obj is ItemStack stack &&
               EqualityComparer<Item>.Default.Equals(item, stack.item);
    }

    public override int GetHashCode()
    {
        return -1566986794 + EqualityComparer<Item>.Default.GetHashCode(item);
    }
}
