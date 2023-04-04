using TS.Model;
using TS.View;

namespace TS.Controller
{
    public class PlayerBalanceController
    {
        private PlayerBalanceView _balanceView;
        private IAccount _playerAccount;

        public PlayerBalanceController(IAccount account, PlayerBalanceView view)
        {
            _balanceView = view;
            _playerAccount = account;
            Subscribe();
            OnBalanceChange(_playerAccount.Balance);
        }

        private void Subscribe()
        {
            _playerAccount.BalanceChanged += OnBalanceChange;
        }

        private void Unsubscribe()
        {
            _playerAccount.BalanceChanged -= OnBalanceChange;
        }

        private void OnBalanceChange(int number)
        {
            _balanceView.UpdateView(number);
        }
    }
}