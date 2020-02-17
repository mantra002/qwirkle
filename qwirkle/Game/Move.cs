using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qwirkle.Game
{
    public class Move
    {
        public Tile Tile { get; set; }
        public Coord Location { get; set; }
        public int Score { get; set; }

        public Move(Tile p, Coord c, int score = 0)
        {
            this.Tile = p;
            this.Location = c;
            this.Score = score;
        }
    }
}
