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

        public void MainMenu()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.Write("TicTacToe - Game Menu\n\n");

            Console.Write("Make a selection:\n");
            Console.Write("[ ] Start Game\n");
            Console.Write("[ ] Exit");

            int line = 1;

            while (true)
            {
                Console.SetCursorPosition(1, line + 2);
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                string key = keyInfo.Key.ToString();

                if (key == "UpArrow" && line + 2 > 3)
                    line--;
                else if (key == "DownArrow" && line + 2 < 4)
                    line++;
                else if (key == "Spacebar" || key == "Enter")
                {
                    switch (line)
                    {
                        case 1:
                            Start();
                            break;
                        case 2:
                            Environment.Exit(0);
                            break; // program can never reach this line, but compiler will complain without it
                    }
                    return;
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

        public bool CheckWin(Player player)
        {
            // I can't help but feel this can be cleaner...
            for (int i = 1; i <= 3; i++)
            {
                int thisRow = cells.Count(cell => cell.Row == i && cell.Owner == player);
                int thisCol = cells.Count(cell => cell.Col == i && cell.Owner == player);

                if (thisRow == 3 || thisCol == 3)
                    return true;
            }

            // If player owns the middle cell, check the corners
            if (cells.Count(cell => cell.Row == 2 && cell.Col == 2 && cell.Owner == player) == 1)
            {
                if (cells.Count(cell => ((cell.Row == 1 && cell.Col == 1) ||
                                        (cell.Row == 3 && cell.Col == 3)) &&
                                        cell.Owner == player) == 2)
                    return true;

                if (cells.Count(cell => ((cell.Row == 1 && cell.Col == 3) ||
                                        (cell.Row == 3 && cell.Col == 1)) &&
                                        cell.Owner == player) == 2)
                    return true;
            }

            return false;
        }
    }
}