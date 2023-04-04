using System.Collections.Generic;
using UnityEngine;

namespace TS.Model
{
    /// <summary>
    /// Reads default character profiles and converts its data to GameData object
    /// </summary>
  
    public class SOProfileParser
    {
        public GameData GetDefaultGameData()
        {
            List<DefaultCharProfileSO> defaultProfiles = LoadSOProfiles();

            var gameData = new GameData();

            foreach (var profile in defaultProfiles)
            {
                var charProfile = new CharProfile(
                    profile.Owner, profile.InventoryCapacity, profile.Balance);

                charProfile.InventoryItems = ParseInventory(profile);

                charProfile.TradePrefs = ParseTradePrefs(profile);

                gameData.Profiles.Add(charProfile);
            }
            return gameData;
        }

        private List<DefaultCharProfileSO> LoadSOProfiles()
        {
            var addedIds = new HashSet<Owner>();
            var rawProfiles = Resources.LoadAll<DefaultCharProfileSO>("DefaultProfiles");
            var uniqProfiles = new List<DefaultCharProfileSO>(rawProfiles.Length);

            foreach (var profile in rawProfiles)
            {
                if (!addedIds.Contains(profile.Owner))
                {
                    addedIds.Add(profile.Owner);
                    uniqProfiles.Add(profile);
                }
                else
                {
                    Debug.Log(
                        $"Profile for {profile.Owner} already exist, '{profile.name}' not loaded");
                }
            }
            return uniqProfiles;
        }

        private List<InventoryItemInfo> ParseInventory(DefaultCharProfileSO profile)
        {
            List<DefaultItemInfo> defaultItems = profile.InventoryItems;
            List<InventoryItemInfo> parsedInventory = new List<InventoryItemInfo>();

            for (int i = 0; i < defaultItems.Count; i++)
            {
                if (defaultItems[i].ItemData == null)
                {
                    continue;
                }

                var itemInfo = new InventoryItemInfo(defaultItems[i].ItemData.Id, i);
                parsedInventory.Add(itemInfo);
            }

            return parsedInventory;
        }

        private TradePreferences ParseTradePrefs(DefaultCharProfileSO profile)
        {
            var tradePreferences = new TradePreferences();
            tradePreferences.BuyFactor = profile.BuyFactor;
            tradePreferences.SellFactor = profile.SellFactor;
            return tradePreferences;
        }
    }
}