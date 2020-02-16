using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qwirkle.Game
{
    public class Move
    {
        public Piece Piece { get; set; }
        public Coord Location { get; set; }
        public int Score { get; set; }

        public Move(Piece p, Coord c, int score = 0)
        {
            this.Piece = p;
            this.Location = c;
            this.Score = score;
        }
    }
}
