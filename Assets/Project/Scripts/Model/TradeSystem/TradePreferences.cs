using System;
using UnityEngine;

namespace TS.Model
{
    [Serializable]
    public class TradePreferences
    {
        [SerializeField] private float _buyFactor = 1;
        [SerializeField] private float _sellFactor = 1;

        public float BuyFactor
        {
            get => _buyFactor;
            set => _buyFactor = value < 0 ? 0 : value;
        }

        public float SellFactor
        {
            get => _sellFactor;
            set => _sellFactor = value < 0 ? 0 : value;
        }
    }
}