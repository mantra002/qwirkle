using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qwirkle
{
    public class Piece
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

        public Piece(Rules.Color color, Rules.Shape shape)
        {
            this.c = color;
            this.s = shape;
        }

        public Piece(int index)
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

        public void PrintFancyCharacter()
        {
            switch (this.Color)
            {
                case Rules.Color.Blue:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case Rules.Color.Green:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case Rules.Color.Orange:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
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
                    Console.Write("●");
                    break;
                case Rules.Shape.Cross:
                    Console.Write("❈");
                    break;
                case Rules.Shape.Diamond:
                    Console.Write("◆");
                    break;
                case Rules.Shape.Square:
                    Console.Write("■");
                    break;
                case Rules.Shape.Star:
                    Console.Write("★");
                    break;
                case Rules.Shape.X:
                    Console.Write("X");
                    break;
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        }
}
