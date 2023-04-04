namespace TS.Model
{
    public interface ICharacter
    {
        public Owner Owner { get; }
        public IInventory Inventory { get; }
        public IAccount Account { get; }
        public ITradeSystem TradeSystem { get; }
        public TradePreferences TradePrefs { get; set; }

        public void TradeWith(ICharacter seller);
    }
}