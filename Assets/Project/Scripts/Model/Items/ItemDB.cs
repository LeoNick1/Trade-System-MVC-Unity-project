using System.Collections.Generic;
using UnityEngine;

namespace TS.Model
{
    /// <summary>
    /// Item data database
    /// </summary>

    public class ItemDB
    {
        private Dictionary<int, IItemData> _itemsData;

        public ItemDB()
        {
            _itemsData = new Dictionary<int, IItemData>();

            LoadItemData();
        }

        public Item CreateItem(int id)
        {
            IItemData itemData = GetItemData(id);

            if (itemData == null)
            {
                Debug.Log($"Cannot create an item with id# {id}");
                return null;
            }
            Item item = new Item(itemData);
            return item;
        }

        public IItemData GetItemData(int id)
        {
            IItemData itemData;
            _itemsData.TryGetValue(id, out itemData);
            if (itemData == null)
            {
                Debug.Log($"ItemData for ID # {id} was not found in database");
            }
            return itemData;
        }

        private void LoadItemData()
        {
            IItemData[] rawItems = Resources.LoadAll<ItemDataSO>("Items");
            foreach (var item in rawItems)
            {
                if (_itemsData.ContainsKey(item.Id))
                {
                    Debug.Log(
                        $"ID # {item.Id} already exists in Items DB, item {item.Name} was not added");
                }
                else
                {
                    _itemsData.Add(item.Id, item);
                }
            }
        }
    }
}