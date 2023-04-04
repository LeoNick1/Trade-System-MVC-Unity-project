using System;
using UnityEngine;

namespace TS.Model
{
    public class Account : IAccount
    {
        public event Action<int> BalanceChanged;

        public Owner Owner { get; private set; }

        public int Balance { get; private set; }

        public Account(Owner owner)
        {
            Owner = owner;
            Balance = 0;
        }

        public void SetBalance(int amount)
        {
            if (amount < 0)
            {
                Debug.Log("Cannot set negative balance");
                return;
            }

            Balance = amount;
            BalanceChanged?.Invoke(Balance);
        }

        public bool TryDeposit(int amount)
        {
            if (amount < 0)
            {
                Debug.Log("Cannot deposit negative values");
                return false;
            }

            Balance += amount;
            BalanceChanged?.Invoke(Balance);
            return true;
        }

        public bool TryWithdraw(int amount)
        {
            if (amount > Balance)
            {
                Debug.Log("Not enough gold");
                return false;
            }

            Balance -= amount;
            BalanceChanged?.Invoke(Balance);
            return true;
        }
    }
}