using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qwirkle
{
    public class Piece
    {
        private Rules.Colors c;
        private Rules.Shapes s;

        public Rules.Colors Color
        {
            get { return c; }
            set { c = value; }
        }

        public Rules.Shapes Shape
        {
            get { return s; }
            set { s = value; }
        }

        public Piece(Rules.Colors color, Rules.Shapes shape)
        {
            this.c = color;
            this.s = shape;
        }
    }
}
