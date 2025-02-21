using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Utils;
using System.Text;

namespace GamePrototype.Units
{
    public sealed class Player : Unit
    {
        private readonly Dictionary<EquipSlot, EquipItem> _equipment = new();

        public Player(string name, uint health, uint maxHealth, uint baseDamage) : base(name, health, maxHealth, baseDamage)
        {            
        }

        public override uint GetUnitDamage()
        {
            if (_equipment.TryGetValue(EquipSlot.Weapon, out var meleeItem) && meleeItem is Weapon meleeWeapon) 
            {
                return BaseDamage + meleeWeapon.Damage;
            }
            if (_equipment.TryGetValue(EquipSlot.RangedWeapon, out var rangedItem) && rangedItem is Weapon rangedWeapon) 
            {
                return BaseDamage + rangedWeapon.Damage;
            }
            return BaseDamage;
        }

        public override void HandleCombatComplete()
        {
            Console.WriteLine("\nChecking inventory for usable items...");
            var items = Inventory.Items.ToList();
            foreach (var item in items)
            {
                if (Health < MaxHealth && item is HealthPotion healthPotion)
                {
                    Console.WriteLine($"Found health potion. Current health: {Health}/{MaxHealth}");
                    Console.WriteLine("Do you want to use it? (Yes/No)");
                    if (Console.ReadLine()?.ToLower() == "yes")
                    {
                        UseEconomicItem(healthPotion);
                    }
                }
                else if (item is Grindstone grindstone && _equipment.TryGetValue(EquipSlot.Weapon, out var weapon))
                {
                    if (weapon.Durability >= weapon.MaxDurability) continue;
                    Console.WriteLine($"Found grindstone. {weapon.Name} durability: {weapon.Durability}/{weapon.MaxDurability}");
                    Console.WriteLine("Do you want to use it? (Yes/No)");
                    if (Console.ReadLine()?.ToLower() == "yes")
                    {
                        UseEconomicItem(grindstone);
                    }
                }
            }
        }

        public override bool AddItemToInventory(Item item)
        {
            if (item is not EquipItem equipItem) return base.AddItemToInventory(item);
            if (_equipment.TryGetValue(equipItem.Slot, out var existingItem))
            {
                Console.WriteLine($"Found existing {existingItem.Name} in slot {equipItem.Slot}");
                Console.WriteLine($"Do you want to replace it with {equipItem.Name}? (Yes/No)");
                    
                if (Console.ReadLine()?.ToLower() == "yes")
                {
                    Console.WriteLine($"Replacing {existingItem.Name} with {equipItem.Name}");
                    base.AddItemToInventory(existingItem);
                    _equipment[equipItem.Slot] = equipItem;
                    return true;
                }

                Console.WriteLine("Keeping existing equipment");
                return base.AddItemToInventory(equipItem);
            }

            _equipment.Add(equipItem.Slot, equipItem);
            Console.WriteLine($"Equipped {equipItem.Name}");
            return true;
        }

        private void UseEconomicItem(EconomicItem economicItem)
        {
            switch (economicItem)
            {
                case HealthPotion healthPotion:
                {
                    var oldHealth = Health;
                    Health += healthPotion.HealthRestore;
                    if (Health > MaxHealth)
                    {
                        Health = MaxHealth;
                    }
                    Inventory.TryRemove(healthPotion);
                    Console.WriteLine($"Used health potion. Health restored from {oldHealth} to {Health}/{MaxHealth}");
                    break;
                }
                case Grindstone grindstone when _equipment.TryGetValue(EquipSlot.Weapon, out var item):
                {
                    if (item is Weapon weapon)
                    {
                        var oldDurability = weapon.Durability;
                        weapon.Repair(5);
                        Inventory.TryRemove(grindstone);
                        Console.WriteLine($"Used grindstone on {weapon.Name}. Durability improved from {oldDurability} to {weapon.Durability}/{weapon.MaxDurability}");
                    }

                    break;
                }
            }
        }

        protected override uint CalculateAppliedDamage(uint damage)
        {
            var reducedDamage = damage;
            if (_equipment.TryGetValue(EquipSlot.Armour, out var bodyArmor) && bodyArmor is Armour armor) 
            {
                reducedDamage -= (uint)(reducedDamage * (armor.Defence / 100f));
                armor.ReduceDurability(1);
            }

            if (!_equipment.TryGetValue(EquipSlot.Helmet, out var headArmor) || headArmor is not Helmet helmet)
                return reducedDamage;
            reducedDamage -= (uint)(reducedDamage * (helmet.Defence / 100f));
            helmet.ReduceDurability(1);

            return reducedDamage;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(Name);
            builder.AppendLine($"Health {Health}/{MaxHealth}");
            builder.AppendLine("Equipment:");
            foreach (var (slot, item) in _equipment)
            {
                builder.AppendLine($"{slot}: {item.Name} (Durability: {item.Durability}/{item.MaxDurability})");
            }
            
            builder.AppendLine("Inventory:");
            var items = Inventory.Items;
            foreach (var t in items)
            {
                builder.AppendLine($"[{t.Name}] : {t.Amount}");
            }
            return builder.ToString();
        }

        public override void AddItemsFromUnitToInventory(Unit unit)
        {
            var items = unit.Inventory.Items.ToList();
            foreach (var item in items)
            {
                if (item is EquipItem equipItem)
                {
                    if (_equipment.TryGetValue(equipItem.Slot, out var existingItem))
                    {
                        Console.WriteLine($"\nFound {equipItem.Name} (Durability: {equipItem.Durability}/{equipItem.MaxDurability})");
                        Console.WriteLine($"Current equipped: {existingItem.Name} (Durability: {existingItem.Durability}/{existingItem.MaxDurability})");
                        Console.WriteLine("Do you want to replace it? (y/n)");

                        if (Console.ReadLine()?.ToLower() != "y") continue;
                        Console.WriteLine($"Replacing {existingItem.Name} with {equipItem.Name}");
                        base.AddItemToInventory(existingItem);
                        _equipment[equipItem.Slot] = equipItem;
                        unit.Inventory.TryRemove(item);
                    }
                    else
                    {
                        Console.WriteLine($"\nFound {equipItem.Name} (Durability: {equipItem.Durability}/{equipItem.MaxDurability})");
                        Console.WriteLine("Do you want to equip it? (y/n)");

                        if (Console.ReadLine()?.ToLower() != "y") continue;
                        _equipment.Add(equipItem.Slot, equipItem);
                        Console.WriteLine($"Equipped {equipItem.Name}");
                        unit.Inventory.TryRemove(item);
                    }
                }
                else
                {
                    if (!base.AddItemToInventory(item)) continue;
                    unit.Inventory.TryRemove(item);
                    Console.WriteLine($"Picked up {item.Name}");
                }
            }
        }
    }
}
