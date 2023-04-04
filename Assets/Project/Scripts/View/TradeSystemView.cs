using TMPro;
using UnityEngine;

namespace TS.View
{
    public class TradeSystemView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _firstTraderNameText;
        [SerializeField] private TMP_Text _secondTraderNameText;

        [Header("Inventories UI")]
        [SerializeField] private InventoryView _firstInventoryView;
        [SerializeField] private InventoryView _secondInventoryView;

        [Header("Item info fields")]
        [SerializeField] private TMP_Text _itemNameText;
        [SerializeField] private TMP_Text _itemPriceText;

        [Header("Player balance info")]
        [SerializeField] private Transform _playerBalanceSocket;
        [SerializeField] private PlayerBalanceView _playerBalanceView;

        public InventoryView FirstInventoryView => _firstInventoryView;
        public InventoryView SecondInventoryView => _secondInventoryView;

        private void OnEnable()
        {
            _playerBalanceView.Relocate(_playerBalanceSocket, _playerBalanceSocket.position);
        }

        private void OnDisable()
        {
            _playerBalanceView.ResetLocation();
        }

        public void ResetItemInfo()
        {
            _itemNameText.text = string.Empty;
            _itemPriceText.text = string.Empty;
        }

        public void UpdateItemInfo(string name, int price)
        {
            _itemNameText.text = name;
            _itemPriceText.text = price.ToString();
        }

        public void UpdateFirstTraderTitle(string name)
        {
            if (name == string.Empty)
            {
                _firstTraderNameText.text = "None";
            }
            _firstTraderNameText.text = name;
        }

        public void UpdateSecondTraderTitle(string name)
        {
            if (name == string.Empty)
            {
                _secondTraderNameText.text = "None";
            }
            _secondTraderNameText.text = name;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}