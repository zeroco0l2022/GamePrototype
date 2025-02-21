namespace GamePrototype.Items.EconomicItems
{
    public abstract class Item
    {
        protected abstract bool Stackable { get; }

        public uint Amount { get; private set; }

        public string Name { get; }

        protected Item(string name)
        {
            Name = name;
            Amount = 1;
        }

        public bool TryStack(Item item)
        {
            if (!Stackable)
            {
                return false;
            }
            Amount++;
            return true;
        }
    }
}
