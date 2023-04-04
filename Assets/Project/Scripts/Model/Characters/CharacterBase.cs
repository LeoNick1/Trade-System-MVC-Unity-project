using System.Collections.Generic;
using UnityEngine;

namespace TS.Model
{
    public class CharacterBase : ISaveable
    {
        private Dictionary<Owner, Character> _characters;
        private CharacterFactory _charFactory;
        private ItemDB _itemDB;

        public CharacterBase(CharacterFactory charFactory, ItemDB itemDB)
        {
            _characters = new Dictionary<Owner, Character>();
            _charFactory = charFactory;
            _itemDB = itemDB;
        }

        public Character GetCharacter(Owner owner)
        {
            Character character;
            if (_characters.TryGetValue(owner, out character) == false)
            {
                Debug.Log($"Character of {owner} not found");
            }
            return character;
        }

        public void LoadData(GameData data)
        {
            foreach (var profile in data.Profiles)
            {
                Character character;

                if (_characters.ContainsKey(profile.Owner))
                {
                    character = _characters[profile.Owner];
                    character.Inventory.Clear();
                }
                else
                {
                    character = _charFactory.CreateCharacter(profile);
                    _characters.Add(character.Owner, character);
                }

                character.Inventory.SetSize(profile.InventoryCapacity);
                AddItemsByInfo(profile.InventoryItems, character.Inventory);
                character.TradePrefs = profile.TradePrefs;
                character.Account.SetBalance(profile.Balance);
            }

            void AddItemsByInfo(List<InventoryItemInfo> itemsInfo, IInventory inventory)
            {
                foreach (InventoryItemInfo itemInfo in itemsInfo)
                {
                    IItem item = _itemDB.CreateItem(itemInfo.Id);

                    bool succeed = inventory.TryAddItemTo(item, itemInfo.SlotIndex);
                    if (!succeed)
                    {
                        Debug.Log($"Cannot add item with id# {itemInfo.Id} to slot# {itemInfo.SlotIndex}");
                    }
                }
            }
        }

        public void SaveData(GameData data)
        {
            foreach (KeyValuePair<Owner, Character> pair in _characters)
            {
                Character character = pair.Value;
                CharProfile profile = new CharProfile(pair.Key, character.Inventory.Capacity,
                    character.Account.Balance);
                profile.InventoryItems = GetItemsInfo(character.Inventory);
                profile.Balance = character.Account.Balance;
                profile.TradePrefs = character.TradePrefs;
                data.Profiles.Add(profile);
            }

            List<InventoryItemInfo> GetItemsInfo(IInventory inventory)
            {
                List<InventoryItemInfo> itemsInfo = new List<InventoryItemInfo>();
                List<InventorySlot> inventorySlots = inventory.GetAllSlots();
                foreach (var slot in inventorySlots)
                {
                    if (!slot.IsEmpty)
                    {
                        var itemData = slot.Item.ItemData;
                        itemsInfo.Add(new InventoryItemInfo(itemData.Id, slot.Index));
                    }
                }

                return itemsInfo;
            }
        }
    }
}