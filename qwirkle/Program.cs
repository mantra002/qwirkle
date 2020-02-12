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
            List<Coords> corSet;
            Coords c;
            b.PrintFancyBoard();
            Piece p = pb.DrawTile();
            while (p != null)
            {
                corSet = b.GetPossibleOpenSquares();
                corSet = b.GetValidSquares(corSet, p);
                if (corSet != null && corSet.Count > 0)
                {
                    c = corSet[r.Next(0, corSet.Count)];
                    b.AddPiece(c.x, c.y, p);
                    b.PrintFancyBoard();
                }
                p = pb.DrawTile();
            }

        }
    }
}
