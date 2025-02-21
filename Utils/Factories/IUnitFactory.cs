using GamePrototype.Units;

namespace GamePrototype.Utils.Factories
{
    public interface IUnitFactory
    {
        Unit CreatePlayer(string name);
        Unit CreateEnemy();
    }
} 