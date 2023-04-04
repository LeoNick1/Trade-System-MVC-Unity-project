namespace TS.Model
{
    public class Item : IItem
    {
        public IItemData ItemData { get; private set; }

        public Item(IItemData itemData)
        {
            ItemData = itemData;
        }
    }
}