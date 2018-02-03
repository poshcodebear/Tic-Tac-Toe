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
            
            while (true)
            {
                game.MakeSelection(player1);
                game.MakeSelection(player2);
            }
        }
    }
}
