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
            // Initialize with new cells
            // This could be moved to a constructor, though it would require a new game object each time,
            // which would be a breaking change...
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
            // Draw the main menu and handle input from player

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
                            // program can never reach this line, but compiler will complain without it:
                            break; 
                    }
                    return;
                }
            }
        }

        public void Start()
        {
            // Starts a new game
            Init();
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.Write("TicTacToe");
            PaintGrid();
        }

        public bool Turn(Player player, Cell cell)
        {
            // Attempts to take a turn for the specified user
            if (cell.Claim(player))
            {
                PaintGrid(player);
                return true;
            }
            return false;
        }

        void PaintGrid() { PaintGrid(null); }

        void PaintGrid(Player player)
        {
            // Generates the grid with claimed cells
            // Note: any changes here that alter the layout MUST include adjustments to MakeSelection()'s actual[...] formuae and movement keys
            string turnMark = player?.Mark.ToString();
            Console.SetCursorPosition(0, 0);
            Console.Write($"TicTacToe - Play for {turnMark}");

            // Would like to make this a bit cleaner
            Console.SetCursorPosition(0, 2);
            foreach (Cell cell in cells)
            {
                if (cell.Col == 1 && cell.Row != 1)
                    Console.Write($"\n{new string('-', 21)}\n");
                else if (cell.Col != 1)
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
            // Locates and returns the cell at the selected position
            foreach (Cell cell in cells)
            {
                if (cell.Row == row && cell.Col == col)
                    return cell;
            }
            return null;
        }

        public void MakeSelection(Player player)
        {
            // Gets input from player to select cell
            string mark = player.Mark.ToString();
            int row = 2;
            int col = 2;

            while (true)
            {
                PaintGrid(player);

                // Translation between actual cell positions on the board and cell definitions
                // Note: alterations to the grid REQUIRE adjustments here
                int actualRow = (row + 2) / 2 - 1;
                int actualCol = (col + 6) / 8;

                // Highlights the cursor position (unless it's claimed already)
                Console.SetCursorPosition(col, row);
                if (!CheckCell(actualRow, actualCol))
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(mark);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(col, row);
                }
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                // Defines movement keys
                // Note: alterations to the grid REQUIRE adjustments here
                string key = keyInfo.Key.ToString();
                if (key == "UpArrow" && row > 2)
                    row -= 2;
                else if (key == "DownArrow" && row < 6)
                    row += 2;
                else if (key == "LeftArrow" && col > 2)
                    col -= 8;
                else if (key == "RightArrow" && col < 18)
                    col += 8;
                else if (key == "Spacebar" || key == "Enter")
                {
                    Cell cell = GetCell(actualRow, actualCol);
                    if (cell != null && Turn(player, cell))
                        return;
                }
            }
        }

        public bool CheckCell(int row, int col)
        {
            // Check to see if current cell is owned
            Cell cell = GetCell(row, col);
            if (cell.Owner != null)
                return true;
            return false;
        }

        public bool CheckWin(Player player)
        {
            // Check to see if the player has won the game
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