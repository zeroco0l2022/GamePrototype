namespace GamePrototype.Items.EconomicItems
{
    public sealed class HealthPotion : EconomicItem
    {
        public uint HealthRestore => 7;
        protected override bool Stackable => false;

        public HealthPotion(string name) : base(name)
        {
        }      
    }
}
