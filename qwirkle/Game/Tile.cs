using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qwirkle.Game
{
    public class Tile
    {
        private Rules.Color c;
        private Rules.Shape s;

        public Rules.Color Color
        {
            get { return c; }
            set { c = value; }
        }

        public Rules.Shape Shape
        {
            get { return s; }
            set { s = value; }
        }

        public Tile(Rules.Color color, Rules.Shape shape)
        {
            this.c = color;
            this.s = shape;
        }

        public Tile(int index)
            :this((Rules.Color) (index % 6), (Rules.Shape) (index / 6))
        {
        }

        public override string ToString()
        {
            return this.c.ToString() + " " + this.s.ToString();
        }

        public int ToIndex()
        {
            return (6 * (int)this.s) + ((int)this.c);
        }

        public void PrintFancyCharacter(int padSize)
        {
            string character = "N";
            switch (this.Color)
            {
                case Rules.Color.Blue:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case Rules.Color.Green:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case Rules.Color.Orange:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case Rules.Color.Purple:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case Rules.Color.Red:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Rules.Color.Yellow:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
            }
            switch (this.Shape)
            {
                case Rules.Shape.Circle:
                    character = "O";
                    break;
                case Rules.Shape.Cross:
                    character = "+";
                    break;
                case Rules.Shape.Diamond:
                    character = "D";
                    break;
                case Rules.Shape.Square:
                    character = "C";
                    break;
                case Rules.Shape.Star:
                    character = "S";
                    break;
                case Rules.Shape.X:
                    character = "X";
                    break;
            }
            Console.Write(character.PadLeft(padSize));
            Console.ForegroundColor = ConsoleColor.White;
        }
        }
}
