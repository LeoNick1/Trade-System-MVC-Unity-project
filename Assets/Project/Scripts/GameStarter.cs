using TS.Controller;
using TS.Model;
using TS.View;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private TradeSystemView _tradeSystemView;
    [SerializeField] private PlayerBalanceView _playerBalanceView;
    [SerializeField] private GamePersisterView _gamePersisterView;
    private PlayerBalanceController _playerBalanceController;
    private TradeSystemController _tradeController;
    private GamePersisterController _gamePersisterController;

    private ItemDB _itemDB;
    private Bank _bank;
    private TradeSystem _tradeSystem;
    private CharacterBase _characterBase;

    private void Awake()
    {
        InitializeGame();
        Character player = _characterBase.GetCharacter(Owner.Player);
        Character trader = _characterBase.GetCharacter(Owner.Trader);
        player.TradeWith(trader);
    }

    private void InitializeGame()
    {
        _itemDB = new ItemDB();
        _bank = new Bank();

        _tradeSystem = new TradeSystem(_bank);
        _tradeController = new TradeSystemController(_tradeSystemView, _tradeSystem);

        InitCharacterBase(_bank, _tradeSystem, _itemDB);

        InitGamePersister(_gamePersisterView);
        _gamePersisterController.AddSaveableObject(_characterBase);
        _gamePersisterController.LoadGame();

        SetPlayerBalanceView(_bank);
    }

    private void InitCharacterBase(Bank bank, TradeSystem tradeSystem, ItemDB itemDB)
    {
        CharacterFactory charFactory = new CharacterFactory(bank, tradeSystem);
        _characterBase = new CharacterBase(charFactory, itemDB);
    }

    private void InitGamePersister(GamePersisterView persisterView)
    {
        var gamePersister = new GamePersister();
        _gamePersisterController = new GamePersisterController(gamePersister, persisterView);
    }

    private void SetPlayerBalanceView(Bank bank)
    {
        _playerBalanceController = new PlayerBalanceController(
            bank.GetAccountOf(Owner.Player), _playerBalanceView);
    }  
}
