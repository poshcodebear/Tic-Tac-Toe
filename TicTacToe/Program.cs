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

            game.Start();

            game.Turn(player1, game.GetCell(1, 0));
            game.Turn(player2, game.GetCell(2, 0));

            Console.ReadKey();
        }
    }
}
