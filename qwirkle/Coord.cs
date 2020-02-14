
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qwirkle
{
    public class Coord
    {
        private int y;
        private int x;

        public Coord(int p1, int p2)
        {
            x = p1;
            y = p2;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
    }
}
