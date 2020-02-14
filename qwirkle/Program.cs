using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qwirkle
{
    class Program
    {
        static void Main(string[] args)
        {

            Random r = new Random();
            PieceBag pb = new PieceBag();
            Board b = new Board();
            List<Coord> corSet;
            Coord c;
            b.PrintFancyBoard();
            Piece p = pb.DrawTile();
            while (p != null)
            {
                corSet = b.GetValidSquares(p);
                if (corSet != null && corSet.Count > 0)
                {
                    c = corSet[r.Next(0, corSet.Count)];
                    b.AddPiece(c.X, c.Y, p);
                    b.PrintFancyBoard();
                    b.EndTurn();
                }
                p = pb.DrawTile();
            }

        }
    }
}
