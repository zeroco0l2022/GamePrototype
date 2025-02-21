using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units;

namespace GamePrototype.Utils.Factories
{
    public class HardUnitFactory : IUnitFactory
    {
        public Unit CreatePlayer(string name)
        {
            var player = new Player(name, 25, 25, 5);
            player.AddItemToInventory(new MeleeWeapon(8, 10, "Rusty Sword"));
            player.AddItemToInventory(new BodyArmour(8, 10, "Leather Armor"));
            return player;
        }

        public Unit CreateEnemy()
        {
            var enemy = new Goblin(GameConstants.Goblin, 25, 25, 4);
            enemy.AddItemToInventory(new RangedWeapon(10, 15, "Old Bow"));
            enemy.AddItemToInventory(new HealthPotion("Health Potion"));
            enemy.AddItemToInventory(new Grindstone("Grindstone"));
            return enemy;
        }
    }
} 