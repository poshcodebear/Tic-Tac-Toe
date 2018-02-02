using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    class Player
    {
        Mark mark;
        public Mark Mark { get => mark; }
        public bool Won
        {
            get
            {
                // Need winning logic
                return false;
            }
        }

        public Player(Mark mark)
        {
            this.mark = mark;
        }
    }
}
