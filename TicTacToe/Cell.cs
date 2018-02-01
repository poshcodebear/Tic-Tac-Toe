using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    class Cell
    {
        int row;
        int col;
        Player owner;

        public int Row { get => row; }
        public int Col { get => col; }
        public Player Owner { get => owner; }

        public Cell(int Row, int Col)
        {
            row = Row;
            col = Col;
        }

        public void Claim(Player player)
        {
            if (owner == null)
                owner = player;
        }
    }
}
