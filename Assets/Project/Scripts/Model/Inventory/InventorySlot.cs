using System;

namespace TS.Model
{
    public class InventorySlot
    {
        public event Action<InventorySlot> SlotUpdated;

        public IItem Item { get; private set; }
        public readonly int Index;
        public bool IsEmpty => Item == null;

        public InventorySlot(int slotIndex)
        {
            Index = slotIndex;
        }

        public void Clear()
        {
            if (IsEmpty)
            {
                return;
            }
            
            Item = null;
            SlotUpdated?.Invoke(this);
        }

        public void PutIn(IItem item)
        {
            Item = item;
            SlotUpdated?.Invoke(this);
        }
    }
}