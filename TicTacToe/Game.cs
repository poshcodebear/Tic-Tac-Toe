using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    class Game
    {
        Cell[] cells;

        bool gameover { get => false; } //needs to be set up to check status of game

        public Game()
        {
            Init();
        }

        void Init()
        {
            cells = new Cell[9];

            int i = 0;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    cells[i] = new Cell(row, col);
                    i++;
                }
            }
        }

        public void Start()
        {
            Init();
            PaintGrid();
        }

        public bool Turn(Player player, Cell cell)
        {
            cell.Claim(player);
            PaintGrid();
            return gameover;
        }

        void PaintGrid()
        {
            foreach (Cell cell in cells)
            {
                if (cell.Col == 0 && cell.Row != 0)
                    Console.WriteLine("\n---------------------");
                else if (cell.Col != 0)
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
    }
}
