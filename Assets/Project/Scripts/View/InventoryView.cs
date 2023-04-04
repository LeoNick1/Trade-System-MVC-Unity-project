using System;
using System.Collections.Generic;
using UnityEngine;

namespace TS.View
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private InventorySlotView _slotPrefab;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private int _startCapacity = 20;

        public event Action<InventorySlotView, InventorySlotView> ItemMoved;
        public event Action<InventorySlotView, InventorySlotView> ItemTransfered;
        public event Action<InventorySlotView> SlotContentRequested;

        public Owner Owner { get; private set; }
        public int Capacity => _slots.Count;

        private List<InventorySlotView> _slots = new List<InventorySlotView>();

        public void Initialize()
        {
            SetSize(_startCapacity);
        }

        public void SetOwner(Owner owner)
        {
            Owner = owner;
        }
        
        public InventorySlotView GetSlot(int slotIndex)
        {
            if (slotIndex > _slots.Count || slotIndex < 0)
            {
                Debug.Log($"Slot index out of inventory capacity");
                return null;
            }

            return _slots[slotIndex];
        }

        public void SetSlotView(int slotIndex, Sprite sprite)
        {
            var slot = GetSlot(slotIndex);
            if (slot == null)
            {
                return;
            }

            slot.SetItemView(sprite);
        }

        public void ClearSlot(int slotIndex)
        {
            var slot = GetSlot(slotIndex);
            slot.Clear();
        }

        public void SetSize(int capacity)
        {
            if (capacity < 0)
            {
                Debug.Log("Inventory capacity cannot be negative");
                return;
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
                    RemoveSlot();
                    delta++;
                }
            }
        }

        private void AddSlot()
        {
            InventorySlotView slot = Instantiate(_slotPrefab, transform);
            slot.Initialize(this, _slots.Count, _canvas);
            slot.ItemMoved += OnItemMoved;
            slot.SlotContentRequested += OnSlotContentRequested;
            _slots.Add(slot);
        }

        private void RemoveSlot()
        {
            if (_slots.Count == 0) return;
            InventorySlotView slot = _slots[_slots.Count - 1];
            slot.ItemMoved -= OnItemMoved;
            slot.SlotContentRequested -= OnSlotContentRequested;
            Destroy(slot.gameObject);
            _slots.RemoveAt(_slots.Count - 1);
        }

        private void Clear()
        {
            foreach (InventorySlotView slot in _slots)
            {
                slot.Clear();
            }
        }

        private void OnItemMoved(InventorySlotView fromSlot, InventorySlotView toSlot)
        {
            if (fromSlot.Owner == toSlot.Owner)
            {
                ItemMoved?.Invoke(fromSlot, toSlot);
            }
            else
            {
                ItemTransfered?.Invoke(fromSlot, toSlot);
            }
        }

        private void OnSlotContentRequested(InventorySlotView slot)
        {
            SlotContentRequested?.Invoke(slot);
        }
    }
}