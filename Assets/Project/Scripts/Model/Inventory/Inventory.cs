using System;
using System.Collections.Generic;
using UnityEngine;

namespace TS.Model
{
    public class Inventory : IInventory
    {
        public event Action<int> InventoryResized;
        public event Action<InventorySlot> SlotUpdated;
        public event Action<List<InventorySlot>> SlotsUpdated;

        public int Capacity => _slots.Count;
        public bool IsFull => GetEmptySlotsCount() == 0;
        public Owner Owner { get; }
        
        private List<InventorySlot> _slots;
        private int _minCapacity = 10;

        public Inventory(Owner owner, int capacity)
        {
            Owner = owner;
            _slots = new List<InventorySlot>(capacity);
            Initialize(capacity);
        }

        private void Initialize(int size)
        {
            SetSize(size);
        }

        public void SetSize(int capacity)
        {
            if (capacity < _minCapacity)
            {
                capacity = _minCapacity;
                Debug.Log("Inventory capacity is lower than minimal,"
                            + "capacity set to " + _minCapacity);
            }

            int delta = capacity - _slots.Count;

            if (delta > 0)
            {
                while (delta > 0)
                {
                    AddSlot();
                    delta--;
                }

            }
            else if (delta < 0)
            {
                while (delta < 0)
                {
                    bool succeed = TryRemoveSlot();
                    if (!succeed)
                    {
                        Debug.Log("Inventory resized to number of items in it");
                        break;
                    }
                    delta++;
                }
            }
            InventoryResized?.Invoke(_slots.Count);
            SlotsUpdated?.Invoke(_slots);
        }

        public InventorySlot GetSlot(int slotIndex)
        {
            if (slotIndex > _slots.Count || slotIndex < 0)
            {
                Debug.Log($"Slot index out of inventory capacity");
                return null;
            }

            return _slots[slotIndex];
        }

        public List<InventorySlot> GetAllSlots()
        {
            var allSlots = new List<InventorySlot>(_slots);
            
            return allSlots;    
        }

        public IItem GetItemInSlot(int slotIndex)
        {
            var slot = GetSlot(slotIndex);

            if (slot == null || slot.IsEmpty)
            {
                return null;
            }

            return slot.Item;
        }

        public void MoveItem(int fromSlotIndex, int toSlotIndex)
        {
            InventorySlot fromSlot = GetSlot(fromSlotIndex);
            if (fromSlot == null)
            {
                Debug.Log("Source slot does not exist");
                return;
            }

            InventorySlot toSlot = GetSlot(toSlotIndex);
            if (fromSlot == null)
            {
                Debug.Log("Destination slot does not exist");
                return;
            }

            if (toSlot.IsEmpty)
            {
                toSlot.PutIn(fromSlot.Item);
                fromSlot.Clear();
            }
            else
            {
                SwapItems(fromSlot, toSlot);
            }
        }

        public bool TryAddItem(IItem item)
        {
            bool succeed = true;
            for (int i = 0; i < _slots.Count; i++)
            {
                if (_slots[i].IsEmpty)
                {
                    _slots[i].PutIn(item);
                    return succeed;
                }
            }
            return !succeed;
        }

        public bool TryAddItemTo(IItem item, int slotIndex)
        {
            bool succeed = true;
            var slot = GetSlot(slotIndex);
            if (slot == null)
            {
                Debug.Log("Destination slot does not exist");
                return !succeed;
            }

            if (!slot.IsEmpty)
            {
                return !succeed;
            }

            slot.PutIn(item);
            return succeed;
        }

        private void AddSlot()
        {
            var slot = new InventorySlot(_slots.Count);
            slot.SlotUpdated += OnSlotUpdate;
            _slots.Add(slot);
        }

        private bool TryRemoveSlot()
        {
            if (_slots.Count == 0) return false;
            var slot = _slots[_slots.Count - 1];
            if (!slot.IsEmpty)
            {
                bool succeed = TryAddItem(slot.Item);
                if (!succeed)
                {
                    Debug.Log("Cannot move item out of slot to remove");
                    return false;
                }
                slot.Clear();
            }
            slot.SlotUpdated -= OnSlotUpdate;
            _slots.RemoveAt(_slots.Count - 1);
            return true;
        }

        private void SwapItems(InventorySlot fromSlot, InventorySlot toSlot)
        {
            IItem toSlotItem = toSlot.Item;

            toSlot.PutIn(fromSlot.Item);
            fromSlot.PutIn(toSlotItem);
        }

        public void ClearSlot(int slotIndex)
        {
            var slotToClear = GetSlot(slotIndex);
            if (slotToClear == null)
            {
                return;
            }
            
            slotToClear.Clear();
        }

        public int GetEmptySlotsCount()
        {
            int emptySlotsCount = 0;
            foreach (var slot in _slots)
            {
                if (slot.IsEmpty)
                {
                    emptySlotsCount++;
                }
            }

            return emptySlotsCount;
        }

        public void Clear()
        {
            foreach (var slot in _slots)
            {
                if (!slot.IsEmpty)
                {
                    slot.Clear();
                }
            }
        }

        private void OnSlotUpdate(InventorySlot slot)
        {
            SlotUpdated?.Invoke(slot);
        }
    }
}