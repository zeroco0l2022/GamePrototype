using GamePrototype.Items.EconomicItems;

namespace GamePrototype.Units
{
    public abstract class Unit
    {
        private const int InventorySize = 3;
        public string Name { get; }
        public uint Health { get; protected set; }
        public uint MaxHealth { get; }
        protected uint BaseDamage { get; }
        public Inventory Inventory { get; }

        protected Unit(string name, uint health, uint maxHealth, uint baseDamage)
        {
            Name = name;
            Health = health;
            MaxHealth = maxHealth;
            BaseDamage = baseDamage;
            Inventory = new Inventory(InventorySize);
        }

        public void ApplyDamage(uint damage)
        {
            var appliedDamage = CalculateAppliedDamage(damage);
            if (Health <= appliedDamage)
            {
                Health = 0;
            }
            else
            {
                Health -= appliedDamage;
            }
            
            DamageReceiveHandler();
        }

        protected virtual uint CalculateAppliedDamage(uint damage) => damage;
        
        protected virtual void DamageReceiveHandler() { }
        
        public abstract uint GetUnitDamage();

        public virtual void HandleCombatComplete() { }

        public virtual bool AddItemToInventory(Item item) => Inventory.TryAdd(item);

        public virtual void AddItemsFromUnitToInventory(Unit unit)
        {
            var items = unit.Inventory.Items.ToList();
            foreach (var item in items)
            {
                if (AddItemToInventory(item))
                {
                    unit.Inventory.TryRemove(item);
                }
            }
        }
    }
}
