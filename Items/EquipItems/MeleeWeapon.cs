using GamePrototype.Utils;

namespace GamePrototype.Items.EquipItems
{
    public sealed class MeleeWeapon : Weapon
    {
        public MeleeWeapon(uint damage, uint durability, string name) : base(damage, durability, name) { }

        public override EquipSlot Slot => EquipSlot.Weapon;
    }
} 