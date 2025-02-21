using GamePrototype.Dungeon;
using GamePrototype.Utils.Factories;

namespace GamePrototype.Utils.Builders
{
    public interface IDungeonBuilder
    {
        DungeonRoom BuildDungeon(IUnitFactory unitFactory);
    }
} 