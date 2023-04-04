using System;
using System.Collections.Generic;

namespace TS.Model
{
    public interface IInventory
    {
        event Action<int> InventoryResized;
        event Action<InventorySlot> SlotUpdated;
        event Action<List<InventorySlot>> SlotsUpdated;

        bool IsFull { get; }
        int Capacity { get; }
        Owner Owner { get; }

        List<InventorySlot> GetAllSlots();
        void SetSize(int capacity);
        IItem GetItemInSlot(int slotIndex);
        void MoveItem(int fromIndex, int toIndex);
        bool TryAddItem(IItem item);
        bool TryAddItemTo(IItem item, int slotIndex);
        void ClearSlot(int slotIndex);
        void Clear();
    }
}