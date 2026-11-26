using System;

[Serializable]
public class InventorySlotData
{
    public int SlotIndex;
    public ItemData Item;
    public InventorySlotData() { }
    public InventorySlotData(int index, ItemData item)
    {
        SlotIndex = index;
        Item = item;
    }
}
