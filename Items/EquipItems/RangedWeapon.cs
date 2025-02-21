using GamePrototype.Utils;

namespace GamePrototype.Items.EquipItems
{
    public sealed class RangedWeapon : Weapon
    {
        public RangedWeapon(uint damage, uint durability, string name) : base(damage, durability, name) { }

        public override EquipSlot Slot => EquipSlot.RangedWeapon;
    }
} 