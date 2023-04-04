using System;
using UnityEngine;

namespace TS.Model
{
    public class TradeSystem : ITradeSystem
    {
        public event Action<IInventory> FirstTraderChanged;
        public event Action<IInventory> SecondTraderChanged;
        
        private ICharacter _firstTrader;
        private ICharacter _secondTrader;
        private Bank _bank;

        public TradeSystem(Bank bank)
        {
            _bank = bank;
        }

        public void PrepareTrade(ICharacter buyer, ICharacter seller)
        {
            SetFirstTrader(buyer);
            SetSecondTrader(seller);
        }

        public void SetFirstTrader(ICharacter trader)
        {
            _firstTrader = trader;
            FirstTraderChanged?.Invoke(_firstTrader.Inventory);
        }

        public void SetSecondTrader(ICharacter trader)
        {
            _secondTrader = trader;
            SecondTraderChanged?.Invoke(_secondTrader.Inventory);
        }

        public void Trade(Owner seller, int sellerSlot, Owner buyer, int buyerSlot)
        {
            ICharacter buyerChar = GetTrader(buyer);
            ICharacter sellerChar = GetTrader(seller);
            if (buyerChar == null || sellerChar == null)
            {
                return;
            }

            var buyerInventory = buyerChar.Inventory;
            var sellerInventory = sellerChar.Inventory;

            IItem item = sellerInventory.GetItemInSlot(sellerSlot);
            if (item == null)
            {
                Debug.Log("Cannot find the item");
                return;
            }

            var buyerPrefs = buyerChar.TradePrefs;
            var sellerPrefs = sellerChar.TradePrefs;

            int itemValue = item.ItemData.Value;
            int itemPrice = Mathf.RoundToInt(itemValue * buyerPrefs.BuyFactor * sellerPrefs.SellFactor);

            bool succeedPayment = _bank.TryToTransact(buyer, seller, itemPrice);

            if (!succeedPayment)
            {
                return;
            }

            bool isAddedDirectly = buyerInventory.TryAddItemTo(item, buyerSlot);
            if (!isAddedDirectly)
            {
                bool isAdded = buyerInventory.TryAddItem(item);
                if (!isAdded)
                {
                    Debug.Log("Cannot add item to buyer");
                    _bank.TryToTransact(seller, buyer, itemPrice);
                    return;
                }
            }

            sellerInventory.ClearSlot(sellerSlot);
        }

        public int GetItemPrice(Owner owner, IItem item)
        {
            int itemValue = item.ItemData.Value;
            int itemPrice;
            float valueMultiplier = 1f;

            if (owner == _firstTrader.Owner)
            {
                valueMultiplier = _firstTrader.TradePrefs.SellFactor * _secondTrader.TradePrefs.BuyFactor;
            }
            else if (owner == _secondTrader.Owner)
            {
                valueMultiplier = _secondTrader.TradePrefs.SellFactor * _firstTrader.TradePrefs.BuyFactor;
            }
            else
            {
                Debug.Log($"{owner} is not trading");
                return itemValue;
            }

            itemPrice = Mathf.RoundToInt(itemValue * valueMultiplier);

            return itemPrice;
        }

        private ICharacter GetTrader(Owner owner)
        {
            if (owner == _firstTrader.Owner)
            {
                return _firstTrader;
            }
            else if (owner == _secondTrader.Owner)
            {
                return _secondTrader;
            }
            else
            {
                Debug.Log($"{owner} is not trading");
                return null;
            }
        }
    }
}