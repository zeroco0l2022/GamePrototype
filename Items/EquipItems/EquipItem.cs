using GamePrototype.Items.EconomicItems;
using GamePrototype.Utils;

namespace GamePrototype.Items.EquipItems
{
    public abstract class EquipItem : Item
    {
        public uint Durability { get; protected set; }

        public uint MaxDurability { get; }

        protected override bool Stackable => false;

        public abstract EquipSlot Slot { get; }

        protected EquipItem(uint maxDurability, string name) : base(name)
        {
            MaxDurability = maxDurability;
            Durability = maxDurability;
        }

        public void ReduceDurability(uint delta)
        {
            if (Durability <= delta)
            {
                Durability = 0;
                Console.WriteLine($"{Name} is broken!");
            }
            else
            {
                Durability -= delta;
            }
        }

        public void Repair(uint delta) => 
            Durability += Durability + delta > MaxDurability 
            ? MaxDurability 
            : Durability + delta;
    }
}
