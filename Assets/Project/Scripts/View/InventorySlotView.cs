using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TS.View
{
    public class InventorySlotView : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private ItemView _itemView;

        public event Action<InventorySlotView, InventorySlotView> ItemMoved;
        public event Action<InventorySlotView> SlotContentRequested;

        public InventoryView ParentInventoryView { get; private set; }
        public ItemView ItemView => _itemView;
        public int Index { get; private set; }
        public Owner Owner => ParentInventoryView.Owner;
        public bool isEmpty => _itemView.IsFree;

        public void Initialize(InventoryView inventoryView, int slotIndex, Canvas canvas)
        {
            Index = slotIndex;
            ParentInventoryView = inventoryView;
            _itemView.Initialize(canvas);
        }

        public void SetItemView(Sprite sprite)
        {
            _itemView.SetView(sprite);
        }

        public void Clear()
        {
            _itemView.Clear();
        }

        public void OnDrop(PointerEventData eventData)
        {
            var otherItemObject = eventData.pointerDrag.gameObject;
            var otherItem = otherItemObject.GetComponent<ItemView>();
            if (otherItem == null)
            {
                return;
            }
            var otherItemSlot = otherItem.ItemSlot;

            ItemMoved?.Invoke(otherItemSlot, this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            SlotContentRequested?.Invoke(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SlotContentRequested?.Invoke(null);
        }
    }
}