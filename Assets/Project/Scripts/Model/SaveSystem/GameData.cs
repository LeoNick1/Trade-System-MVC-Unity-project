using System;
using System.Collections.Generic;

namespace TS.Model
{
    /// <summary>
    /// Game data for serialization
    /// </summary>
    
    [Serializable]
    public class GameData
    {
        public List<CharProfile> Profiles;

        public GameData()
        {
            Profiles = new List<CharProfile>();
        }
    }

    // Character data
    [Serializable]
    public class CharProfile
    {
        public Owner Owner;
        public List<InventoryItemInfo> InventoryItems;
        public int InventoryCapacity;
        public int Balance;
        public TradePreferences TradePrefs;

        public CharProfile(Owner owner, int inventoryCapacity, int balance)
        {
            Owner = owner;
            InventoryCapacity = inventoryCapacity;
            Balance = balance;
        }

    }

    // Items in inventory
    [Serializable]
    public class InventoryItemInfo
    {
        public int Id;
        public int SlotIndex;

        public InventoryItemInfo(int itemId, int slotIndex)
        {
            Id = itemId;
            SlotIndex = slotIndex;
        }
    }
}