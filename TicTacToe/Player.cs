using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    class Player
    {
        Cell[] cells;
        Mark mark;
        public Mark Mark { get => mark; }

        public Player(Mark mark)
        {
            this.mark = mark;
        }
    }
}
