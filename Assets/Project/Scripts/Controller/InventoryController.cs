using System.Collections.Generic;
using TS.Model;
using TS.View;

namespace TS.Controller
{
    public class InventoryController
    {
        private InventoryView _inventoryView;
        private IInventory _inventoryModel;

        public InventoryController(InventoryView inventoryView)
        {
            _inventoryView = inventoryView;
            inventoryView.Initialize();
        }

        public void SetModel(IInventory inventoryModel)
        {
            if (_inventoryModel != null)
            {
                Unsubscribe();
            }

            _inventoryModel = inventoryModel;
            Subscribe();
            UpdateInventoryView();
        }

        private void Subscribe()
        {
            _inventoryView.ItemMoved += OnItemViewMoved;
            _inventoryModel.SlotUpdated += OnSlotUpdate;
            _inventoryModel.SlotsUpdated += OnSlotsUpdate;
            _inventoryModel.InventoryResized += OnInventoryResize;
        }

        private void Unsubscribe()
        {
            _inventoryView.ItemMoved -= OnItemViewMoved;
            _inventoryModel.SlotUpdated -= OnSlotUpdate;
            _inventoryModel.SlotsUpdated -= OnSlotsUpdate;
            _inventoryModel.InventoryResized -= OnInventoryResize;
        }

        private void UpdateInventoryView()
        {
            if (_inventoryModel == null)
            {
                return;
            } 
            _inventoryView.SetOwner(_inventoryModel.Owner);
            _inventoryView.SetSize(_inventoryModel.Capacity);
            OnSlotsUpdate(_inventoryModel.GetAllSlots());
        }

        public IItem GetItemInSlot(int slotIndex)
        {
            IItem item = _inventoryModel.GetItemInSlot(slotIndex);
            return item;
        }

        private void OnItemViewMoved(InventorySlotView fromSlot, InventorySlotView toSlot)
        {
            _inventoryModel.MoveItem(fromSlot.Index, toSlot.Index);
        }

        private void OnSlotUpdate(InventorySlot slot)
        {
            if (slot.IsEmpty)
            {
                _inventoryView.ClearSlot(slot.Index);
            }
            else
            {
                _inventoryView.SetSlotView(slot.Index, slot.Item.ItemData.Icon);
            }
        }

        private void OnSlotsUpdate(List<InventorySlot> slots)
        {
            foreach (var slot in slots)
            {
                OnSlotUpdate(slot);
            }
        }

        private void OnInventoryResize(int capacity)
        {
            _inventoryView.SetSize(capacity);
        }

        
    }
}