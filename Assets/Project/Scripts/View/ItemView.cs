using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TS.View
{
    public class ItemView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private RectTransform _itemRectTransform;
        [SerializeField] private InventorySlotView _itemSlot;
        [SerializeField] private Image _itemImage;

        public InventorySlotView ItemSlot => _itemSlot;
        public bool IsFree { get; private set; } = true;

        private Canvas _canvas;
        private Transform _originalParent;

        public void Initialize(Canvas canvas)
        {
            _canvas = canvas;
        }
        public void SetView(Sprite sprite)
        {
            _itemImage.sprite = sprite;
            _itemImage.enabled = true;
            IsFree = false;
        }

        public void Clear()
        {
            _itemImage.sprite = null;
            _itemImage.enabled = false;
            IsFree = true;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _itemImage.raycastTarget = false;
            _originalParent = transform.parent;
            transform.SetParent(_canvas.transform);
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform,
                Input.mousePosition, _canvas.worldCamera, out position);

            transform.position = _canvas.transform.TransformPoint(position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _itemRectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _itemImage.raycastTarget = true;
            transform.SetParent(_originalParent);
            transform.localPosition = Vector3.zero;
        }
    }
}