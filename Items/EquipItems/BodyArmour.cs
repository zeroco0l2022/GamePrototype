using GamePrototype.Utils;

namespace GamePrototype.Items.EquipItems
{
    public sealed class BodyArmour : Armour
    {
        public BodyArmour(uint defence, uint durability, string name) : base(defence, durability, name) { }

        public override EquipSlot Slot => EquipSlot.Armour;
    }
} 