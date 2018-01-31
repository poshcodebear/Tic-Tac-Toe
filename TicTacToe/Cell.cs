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
        Owner selection;

        public int Row { get => row; }
        public int Col { get => col; }
        public Owner Selection { get => selection; }

        Cell(int Row, int Col)
        {
            row = Row;
            col = Col;
            selection = Owner.None;
        }

        void SetOwner(Owner selection)
        {
            if (this.selection == Owner.None)
                this.selection = selection;
        }
    }

    enum Owner
    {
        X, Y, None
    }
}
