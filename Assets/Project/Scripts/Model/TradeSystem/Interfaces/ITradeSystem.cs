namespace TS.Model
{
    public interface ITradeSystem
    {
        public void PrepareTrade(ICharacter buyer, ICharacter seller);
    }
}