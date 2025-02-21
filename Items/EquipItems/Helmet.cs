using GamePrototype.Utils;

namespace GamePrototype.Items.EquipItems
{
    public sealed class Helmet : Armour
    {
        public Helmet(uint defence, uint durability, string name) : base(defence, durability, name) { }

        public override EquipSlot Slot => EquipSlot.Helmet;
    }
} 