using System.Collections.Generic;
using UnityEngine;

namespace TS.Model
{
    /// <summary>
    /// Default character data SO to design in Unity editor
    /// </summary>

    [CreateAssetMenu(menuName = "ScriptableObject/Character Profile", fileName = "Character Profile")]
    public class DefaultCharProfileSO : ScriptableObject
    {
        [SerializeField] private Owner _owner;
        [SerializeField] private int _balance;

        [Header("List of inventory items")]
        [SerializeField] private List<DefaultItemInfo> _inventoryItems = new List<DefaultItemInfo>();

        [SerializeField] private int _inventoryCapacity = 20;

        [Header("Item prices multiplication for buying and selling")]
        [SerializeField] [Range(0, 3f)] private float _buyFactor = 1;
        [SerializeField] [Range(0, 3f)] private float _sellFactor = 1;

        public Owner Owner => _owner;
        public int Balance => _balance;
        public List<DefaultItemInfo> InventoryItems => _inventoryItems;
        public int InventoryCapacity => _inventoryCapacity;
        public float BuyFactor => _buyFactor;
        public float SellFactor => _sellFactor;

        private void OnValidate()
        {
            ValidateBalance();
            ValidateInventoryCapacity();
        }

        private void ValidateBalance()
        {
            if (_balance < 0)
            {
                _balance = 0;
                Debug.Log("Balance can not be lower than 0");
            }
        }

        private void ValidateInventoryCapacity()
        {
            if (_inventoryCapacity < _inventoryItems.Count)
            {
                _inventoryCapacity = _inventoryItems.Count;
            }
        }
    }
}