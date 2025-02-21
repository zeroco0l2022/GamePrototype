using GamePrototype.Dungeon;
using GamePrototype.Items.EconomicItems;
using GamePrototype.Units;
using GamePrototype.Utils.Factories;

namespace GamePrototype.Utils.Builders
{
    public class HardDungeonBuilder : IDungeonBuilder
    {
        public DungeonRoom BuildDungeon(IUnitFactory unitFactory)
        {
            var enter = new DungeonRoom("Enter");
            var firstMonster = new DungeonRoom("First Monster", unitFactory.CreateEnemy());
            var secondMonster = new DungeonRoom("Second Monster", unitFactory.CreateEnemy());
            var thirdMonster = new DungeonRoom("Third Monster", unitFactory.CreateEnemy());
            var lootRoom1 = new DungeonRoom("Treasure Room", new Grindstone("Grindstone"));
            var lootRoom2 = new DungeonRoom("Healing Room", new HealthPotion("Health Potion"));
            var finalRoom = new DungeonRoom("Final Room", new Gold());
            
            enter.TrySetDirection(Direction.Right, firstMonster);
            enter.TrySetDirection(Direction.Left, lootRoom1);
            
            firstMonster.TrySetDirection(Direction.Forward, secondMonster);
            firstMonster.TrySetDirection(Direction.Right, lootRoom2);
            
            lootRoom1.TrySetDirection(Direction.Forward, secondMonster);
            lootRoom2.TrySetDirection(Direction.Forward, thirdMonster);
            
            secondMonster.TrySetDirection(Direction.Forward, thirdMonster);
            thirdMonster.TrySetDirection(Direction.Forward, finalRoom);

            return enter;
        }
    }
} 