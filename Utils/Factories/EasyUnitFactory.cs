using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units;

namespace GamePrototype.Utils.Factories
{
    public class EasyUnitFactory : IUnitFactory
    {
        public Unit CreatePlayer(string name)
        {
            var player = new Player(name, 40, 40, 8);
            player.AddItemToInventory(new MeleeWeapon(12, 20, "Steel Sword"));
            player.AddItemToInventory(new BodyArmour(15, 20, "Steel Armor"));
            return player;
        }

        public Unit CreateEnemy()
        {
            var enemy = new Goblin(GameConstants.Goblin, 15, 15, 2);
            enemy.AddItemToInventory(new MeleeWeapon(6, 10, "Rusty Sword"));
            enemy.AddItemToInventory(new HealthPotion("Health Potion"));
            enemy.AddItemToInventory(new Grindstone("Grindstone"));
            return enemy;
        }
    }
} 