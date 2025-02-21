using GamePrototype.Utils;

namespace GamePrototype.Items.EquipItems
{
    public abstract class Weapon : EquipItem
    {
        protected Weapon(uint damage, uint durability, string name) : base(durability, name) => Damage = damage;

        public uint Damage { get; }

        public override EquipSlot Slot => EquipSlot.Weapon;

        public new void Repair(uint amount)
        {
            if (Durability + amount > MaxDurability)
            {
                Durability = MaxDurability;
            }
            else
            {
                Durability += amount;
            }
            Console.WriteLine($"{Name} repaired. Durability: {Durability}/{MaxDurability}");
        }
    }
}
