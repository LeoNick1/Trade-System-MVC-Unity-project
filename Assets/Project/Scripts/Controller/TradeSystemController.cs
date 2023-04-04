using TS.Model;
using TS.View;

namespace TS.Controller
{
    public class TradeSystemController
    {
        private TradeSystemView _tradeView;
        private InventoryView _firstInventoryView;
        private InventoryView _secondInventoryView;
        private InventoryController _firstInventoryController;
        private InventoryController _secondInventoryController;

        private TradeSystem _tradeModel;

        public TradeSystemController(TradeSystemView view, TradeSystem model)
        {
            _tradeView = view;
            _firstInventoryView = _tradeView.FirstInventoryView;
            _secondInventoryView = _tradeView.SecondInventoryView;
            _tradeModel = model;
            InventoryControllersInit();
            Subscribe();
        }

        private void Subscribe()
        {
            _tradeModel.FirstTraderChanged += OnFirstTraderChange;
            _tradeModel.SecondTraderChanged += OnSecondTraderChange;
            _firstInventoryView.SlotContentRequested += UpdateItemTradeInfo;
            _secondInventoryView.SlotContentRequested += UpdateItemTradeInfo;
            _firstInventoryView.ItemTransfered += OnTradeRequest;
            _secondInventoryView.ItemTransfered += OnTradeRequest;
        }

        private void Unsubscribe()
        {
            _tradeModel.FirstTraderChanged -= OnFirstTraderChange;
            _tradeModel.SecondTraderChanged -= OnSecondTraderChange;
            _firstInventoryView.SlotContentRequested -= UpdateItemTradeInfo;
            _secondInventoryView.SlotContentRequested -= UpdateItemTradeInfo;
            _firstInventoryView.ItemTransfered -= OnTradeRequest;
            _secondInventoryView.ItemTransfered -= OnTradeRequest;
        }

        private void InventoryControllersInit()
        {
            _firstInventoryController = new InventoryController(_firstInventoryView);
            _secondInventoryController = new InventoryController(_secondInventoryView);
        }

        private void OnTradeRequest(InventorySlotView sellerSlot, InventorySlotView buyerSlot)
        {
            _tradeModel.Trade(sellerSlot.Owner, sellerSlot.Index, buyerSlot.Owner, buyerSlot.Index);
        }

        private void OnFirstTraderChange(IInventory inventory)
        {
            _firstInventoryController.SetModel(inventory);
            string inventoryOwnerName = string.Empty;
            if (inventory != null)
            {
                inventoryOwnerName = inventory.Owner.ToString();
            }

            _tradeView.UpdateFirstTraderTitle(inventoryOwnerName);
        }

        private void OnSecondTraderChange(IInventory inventory)
        {
            _secondInventoryController.SetModel(inventory);
            string inventoryOwnerName = string.Empty;
            if (inventory != null)
            {
                inventoryOwnerName = inventory.Owner.ToString();
            } 
            
            _tradeView.UpdateSecondTraderTitle(inventoryOwnerName);
        }

        private void UpdateItemTradeInfo(InventorySlotView slot)
        {
            if (slot == null || slot.isEmpty)
            {
                _tradeView.ResetItemInfo();
                return;
            }

            IItem item;

            if (slot.ParentInventoryView == _firstInventoryView)
            {
                item = _firstInventoryController.GetItemInSlot(slot.Index);
            }
            else if (slot.ParentInventoryView == _secondInventoryView)
            {
                item = _secondInventoryController.GetItemInSlot(slot.Index);
            }
            else
            {
                return;
            }

            if (item == null)
            {
                return;
            }

            int itemPrice = _tradeModel.GetItemPrice(slot.Owner, item);

            _tradeView.UpdateItemInfo(item.ItemData.Name, itemPrice);
        }
    }
}