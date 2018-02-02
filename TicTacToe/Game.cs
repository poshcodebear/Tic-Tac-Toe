using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    class Game
    {
        Cell[] cells;

        public Game()
        {
            Init();
        }

        void Init()
        {
            cells = new Cell[9];

            int i = 0;
            for (int row = 1; row <= 3; row++)
            {
                for (int col = 1; col <= 3; col++)
                {
                    cells[i] = new Cell(row, col);
                    i++;
                }
            }
        }

        public void Start()
        {
            Init();
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.Write("TicTacToe");
            PaintGrid();
        }

        public bool Turn(Player player, Cell cell)
        {
            if (cell.Claim(player))
            {
                PaintGrid();
                return true;
            }
            return false;
        }

        void PaintGrid()
        {
            //Console.Write(new string('\b', 110));
            Console.SetCursorPosition(0, 2);
            Console.Write("    1       2       3\n");
            foreach (Cell cell in cells)
            {
                if (cell.Col == 1 && cell.Row != 1)
                    Console.Write($"\n{new string('-', 23)}\n");
                if (cell.Col == 1)
                    Console.Write($"{cell.Row} ");
                else
                    Console.Write(" | ");

                string mark;
                if (cell.Owner == null)
                    mark = " ";
                else
                    mark = cell.Owner.Mark.ToString();

                Console.Write($"[ {mark} ]");
            }
            Console.Write("\n\n");
        }

        public Cell GetCell(int row, int col)
        {
            foreach (Cell cell in cells)
            {
                if (cell.Row == row && cell.Col == col)
                    return cell;
            }
            return null; // should never happen...should...
        }

        public void PromptTurn(Player player)
        {
            bool turnDone;
            do
            {
                Console.SetCursorPosition(0, 10);
                Console.WriteLine($"Turn for player {player.Mark}!");
                Console.Write("Make your selection:    \n \b");
                string x = Convert.ToString(Console.ReadLine());
                Console.SetCursorPosition(21, 11);
                Console.Write($"{x}\n \b");
                string y = Convert.ToString(Console.ReadLine());

                // Need to validate input (1 character between 1 and 3)
                turnDone = Turn(player, GetCell(Convert.ToInt32(x), Convert.ToInt32(y)));
            }
            while (!turnDone);
        }
    }
}
