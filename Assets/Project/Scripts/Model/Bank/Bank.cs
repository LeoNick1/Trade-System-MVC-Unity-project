using System.Collections.Generic;
using UnityEngine;

namespace TS.Model
{
    public class Bank
    {
        private Dictionary<Owner, IAccount> _accounts;

        public Bank()
        {
            _accounts = new Dictionary<Owner, IAccount>();
        }

        public IAccount GetAccountOf(Owner owner)
        {
            IAccount account;
            bool succeed = _accounts.TryGetValue(owner, out account);
            if (!succeed)
            {
                Debug.Log($"Account of {owner} does not exist");
            }
            return account;
        }

        public IAccount CreateAccount(Owner owner)
        {
            if (!_accounts.ContainsKey(owner))
            {
                _accounts.Add(owner, new Account(owner));
            }

            return _accounts[owner];
        }

        public bool TryToTransact(Owner sender, Owner receiver, int amount)
        {
            var senderAccount = GetAccountOf(sender);
            var receiverAccount = GetAccountOf(receiver);

            if (senderAccount == null || receiverAccount == null)
            {
                return false;
            }

            int senderBalance = senderAccount.Balance;
            int receiverBalance = receiverAccount.Balance;

            bool succeed = TryWithdrawFrom(senderAccount, amount);
            if (!succeed)
            {
                return false;
            }

            succeed = TryDepositTo(receiverAccount, amount);
            if (!succeed)
            {
                senderAccount.SetBalance(senderBalance);
                return false;
            }

            return succeed;
        }

        public bool TryDepositTo(IAccount account, int amount)
        {
            if (account == null)
            {
                return false;
            }

            bool succeed = account.TryDeposit(amount);
            return succeed;
        }

        public bool TryWithdrawFrom(IAccount account, int amount)
        {
            if (account == null)
            {
                return false;
            }
            bool succeed = account.TryWithdraw(amount);
            return succeed;
        }
    }
}