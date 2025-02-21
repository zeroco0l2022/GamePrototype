using GamePrototype.Units;

namespace GamePrototype.Combat
{
    public sealed class CombatManager
    {
        private readonly Random _random = new();
        
        public Unit StartCombat(Unit player, Unit enemy)
        {
            var winner = PlayCombatRoutine(player, enemy);
            return winner ?? player;
        }

        private Unit? PlayCombatRoutine(Unit player, Unit enemy)
        {
            Console.WriteLine(GetCombatString());
            while (player.Health > 0 && enemy.Health > 0) 
            {
                if (Enum.TryParse<RockPaperScissors>(Console.ReadLine(), out var rockPaperScissors)) 
                {
                    HandleCombatInput(player, enemy, rockPaperScissors);
                }
                else
                {
                    Console.WriteLine(GetCombatString());
                }
            }

            return player.Health switch
            {
                > 0 when enemy.Health == 0 => player,
                0 when enemy.Health > 0 => enemy,
                _ => null
            };
        }

        private string GetCombatString() => $"Type {RockPaperScissors.Rock} = {(int)RockPaperScissors.Rock}" +
            $"or {RockPaperScissors.Paper} = {(int)RockPaperScissors.Paper}" +
            $"or {RockPaperScissors.Scissors} = {(int)RockPaperScissors.Scissors}";

        private void HandleCombatInput(Unit player, Unit enemy, RockPaperScissors rockPaperScissors)
        {
            var enemyInput = (RockPaperScissors) _random.Next(1, 3);
            Console.WriteLine($"Result player = {rockPaperScissors} and enemy = {enemyInput}");
            switch (rockPaperScissors) 
            {
                case RockPaperScissors.Rock when enemyInput == RockPaperScissors.Scissors:
                case RockPaperScissors.Scissors when enemyInput == RockPaperScissors.Paper:
                case RockPaperScissors.Paper when enemyInput == RockPaperScissors.Rock:
                    ApplyDamage(player, enemy);
                    break;
                case RockPaperScissors.Scissors when enemyInput == RockPaperScissors.Rock:
                case RockPaperScissors.Paper when enemyInput == RockPaperScissors.Scissors:
                case RockPaperScissors.Rock when enemyInput == RockPaperScissors.Paper:
                    ApplyDamage(enemy, player);
                    break;
                default:
                    Console.WriteLine("Combatants tried to hit, but missed :(");
                    break;
            }
        }

        private void ApplyDamage(Unit attacker, Unit defender)
        {
            defender.ApplyDamage(attacker.GetUnitDamage());
            Console.WriteLine($"{attacker.Name} hits {defender.Name}. {defender.Name} health {defender.Health}/{defender.MaxHealth}");
            if (defender.Health == 0) 
            {
                Console.WriteLine($"{defender.Name} is dead!");
            }
        }
    }
}
