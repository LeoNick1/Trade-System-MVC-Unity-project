using System;

namespace TS.Model
{
    public interface IAccount
    {
        event Action<int> BalanceChanged;
        Owner Owner { get; }
        int Balance { get; }

        void SetBalance(int amount);
        bool TryDeposit(int amount);
        bool TryWithdraw(int amount);
    }
}