namespace TS.Model
{
    public class CharacterFactory
    {
        private Bank _bank;
        private TradeSystem _tradeSystem;

        public CharacterFactory(Bank bank, TradeSystem tradeSystem)
        {
            _bank = bank;
            _tradeSystem = tradeSystem;
        }

        public Character CreateCharacter(CharProfile charProfile)
        {
            Character character;
            IAccount account = _bank.CreateAccount(charProfile.Owner);
            character = new Character(
                charProfile.Owner, charProfile.InventoryCapacity, account, _tradeSystem);

            return character;
        }
    }
}