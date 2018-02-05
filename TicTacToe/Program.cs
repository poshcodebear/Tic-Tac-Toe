using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            Player player1 = new Player(Mark.X);
            Player player2 = new Player(Mark.O);

            while (true)
            {
                game.MainMenu();
            
                while (true)
                {
                    game.MakeSelection(player1);
                    if (game.CheckWin(player1))
                    {
                        Console.WriteLine("Player 1 wins");
                        break;
                    }
                    game.MakeSelection(player2);
                    game.CheckWin(player2);
                    if (game.CheckWin(player2))
                    {
                        Console.WriteLine("Player 2 wins");
                        break;
                    }
                }
                Console.Write("Press any key to continue");
                Console.ReadKey();
            }
        }
    }
}
