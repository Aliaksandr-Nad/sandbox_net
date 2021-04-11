using System;
using System.Linq;

namespace RockPaperScissors
{
    class Program
    {
        static void Main(string[] initial)
        {
            if (initial.Length % 2 != 1)
            {
                Console.WriteLine("Invalid arguments. The value must be odd.");
                return;
            }

            if (initial.Length < 3)
            {
                Console.WriteLine("Invalid arguments. The values must be greater than three.");
                return;
            }

            if (initial.GroupBy(x => x).Any(x => x.Count() != 1))
            {
                Console.WriteLine("Invalid arguments. Values mustn't be duplicated");
                return;
            }

            var gameReferee = new GameReferee { Moves = initial};
            gameReferee.PlayGame();
        }
    }
}