namespace GamePrototype.Items.EconomicItems
{
    public sealed class Grindstone : EconomicItem
    {
        protected override bool Stackable => false;

        public Grindstone(string name) : base(name)
        {
        }    
    }
}
