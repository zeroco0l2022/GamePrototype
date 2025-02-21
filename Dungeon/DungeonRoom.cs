using GamePrototype.Items.EconomicItems;
using GamePrototype.Units;

namespace GamePrototype.Dungeon
{
    public sealed class DungeonRoom
    {
        private readonly string _name;
        public readonly Unit? Enemy;
        public readonly Item? Loot;
        public readonly Dictionary<Direction, DungeonRoom> Rooms = new();
        public bool IsFinal => Rooms.Count == 0;

        public DungeonRoom(string name) 
        {
            _name = name;
            Enemy = null;
            Loot = null;
        }

        public DungeonRoom(string name, Unit enemy)
        {
            _name = name;
            Enemy = enemy;
            Loot = null;
        }

        public DungeonRoom(string name, Item loot)
        {
            _name = name;
            Enemy = null;
            Loot = loot;
        }

        public bool TrySetDirection(Direction direction, DungeonRoom room) 
        {
            if (Rooms.TryAdd(direction, room)) return true;
            Console.WriteLine($"Room {_name} already has room for {direction.ToString()}");
            return false;

        }
    }
}
