namespace TS.Model
{
    public class Character : ICharacter
    {
        public Owner Owner { get; private set; }
        public IInventory Inventory { get; private set; }
        public IAccount Account { get; private set; }
        public ITradeSystem TradeSystem { get; private set; }
        public TradePreferences TradePrefs { get; set; }

        public Character(Owner owner, int inventorySize, IAccount account, ITradeSystem tradeSystem)
        {
            Owner = owner;
            Inventory = new Inventory(owner, inventorySize);
            TradeSystem = tradeSystem;
            TradePrefs = new TradePreferences();
            Account = account;
        }

        public void TradeWith(ICharacter seller)
        {
            TradeSystem.PrepareTrade(this, seller);
        }
    }
}