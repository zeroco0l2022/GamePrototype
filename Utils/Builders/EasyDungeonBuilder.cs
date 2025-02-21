using GamePrototype.Dungeon;
using GamePrototype.Items.EconomicItems;
using GamePrototype.Utils.Factories;

namespace GamePrototype.Utils.Builders
{
    public class EasyDungeonBuilder : IDungeonBuilder
    {
        public DungeonRoom BuildDungeon(IUnitFactory unitFactory)
        {
            var enter = new DungeonRoom("Enter");
            var monsterRoom = new DungeonRoom("Monster Room", unitFactory.CreateEnemy());
            var treasureRoom = new DungeonRoom("Treasure Room", new HealthPotion("Health Potion"));
            var secondMonster = new DungeonRoom("Second Monster", unitFactory.CreateEnemy());
            var finalRoom = new DungeonRoom("Final Room", new Gold());
            
            enter.TrySetDirection(Direction.Forward, monsterRoom);
            monsterRoom.TrySetDirection(Direction.Right, treasureRoom);
            monsterRoom.TrySetDirection(Direction.Forward, secondMonster);
            treasureRoom.TrySetDirection(Direction.Forward, secondMonster);
            secondMonster.TrySetDirection(Direction.Forward, finalRoom);

            return enter;
        }
    }
} 