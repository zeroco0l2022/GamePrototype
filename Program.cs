using GamePrototype.Game;
using GamePrototype.Utils;

namespace GamePrototype
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Select difficulty (0 - Easy, 1 - Hard):");
            var difficulty = Console.ReadLine()?.ToLower() == "1" ? Difficulty.Hard : Difficulty.Easy;
            new GameLoop(difficulty).StartGame();
        }
    }
}