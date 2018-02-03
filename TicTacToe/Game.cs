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
            // Would like to make this a bit cleaner
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
            return null;
        }

        public void MakeSelection(Player player)
        {
            string mark = player.Mark.ToString();
            int row = 3;
            int col = 4;

            while (true)
            {
                PaintGrid();
                Console.SetCursorPosition(col, row);
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                string key = keyInfo.Key.ToString();

                if (key == "UpArrow" && row > 3)
                    row -= 2;
                else if (key == "DownArrow" && row < 7)
                    row += 2;
                else if (key == "LeftArrow" && col > 4)
                    col -= 8;
                else if (key == "RightArrow" && col < 20)
                    col += 8;
                else if (key == "Spacebar" || key == "Enter")
                {
                    int actualRow = (row + 1) / 2 - 1;
                    int actualCol = (col + 4) / 8;
                    Cell cell = GetCell(actualRow, actualCol);
                    if (cell != null && Turn(player, cell))
                        return;
                }

            }

        }
    }
}