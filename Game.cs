using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunting_the_Manticore
{
    internal class Game
    {
        short numberOfMoves = 15;
        short manticoreCoordinate;
        short manticoreHealth = 10;
        Player winner = Player.First;

        public void Start()
        {
            GetFirstPlayerNumberAndPassTurnToSecondPlayer();
            LoopTheGame();
            GameOver();
        }

        public void GetFirstPlayerNumberAndPassTurnToSecondPlayer()
        {
            Console.Write("Player 1, how far away from the city do you want to station the Manticore? ");
            manticoreCoordinate = short.Parse(Console.ReadLine() ?? "0");
            Console.WriteLine("Player 2, it is your turn.");
        }

        public void LoopTheGame()
        {
            for (short i = 0; i < numberOfMoves; i++)
            {
                short cannonDamage = CountCannonDamage((short)(i + 1));

                Console.WriteLine("-----------------------------------------------------------");
                Console.WriteLine($"STATUS: Round: {i + 1}  City: {numberOfMoves - i}/15  Manticore: {manticoreHealth}/10");
                Console.WriteLine($"The cannon is expected to deal {cannonDamage} damage this round.");
                Console.Write($"Enter desired cannon range: ");

                short number = short.Parse(Console.ReadLine() ?? "0");

                if (number > manticoreCoordinate)
                {
                    Console.WriteLine("That round OVERSHOT the target.");
                }
                else if (number < manticoreCoordinate)
                {
                    Console.WriteLine("That round FELL SHORT of the target.");
                }
                else
                {
                    Console.WriteLine("That round was a DIRECT HIT!");
                    manticoreHealth -= cannonDamage;
                    if (manticoreHealth == 0) break;
                }
            }

            if (manticoreHealth == 0) winner = Player.Second;
        }

        public void GameOver()
        {
            switch (winner)
            {
                case Player.First:
                    Console.WriteLine("The Manticore hasn't been destroyed! The city of Consolas is doomed!");
                    break;
                case Player.Second:
                    Console.WriteLine("The Manticore has been destroyed! The city of Consolas has been saved!");
                    break;
            }
        }

        public short CountCannonDamage(short stepNumber)
        {
            if (stepNumber % 5 == 0 && stepNumber % 3 == 0) return 10;
            else if (stepNumber % 3 == 0 || stepNumber % 5 == 0) return 3;

            return 1;
        }
    }
}
