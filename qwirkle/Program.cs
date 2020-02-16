using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Qwirkle
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int moveCount = 0;
            Random r = new Random();
            PieceBag pb = new PieceBag();
            Board b = new Board();
            List<Coord> corSet;
            Coord c;
            //Debug.AutoFlush = true;
            b.PrintFancyBoard();
            Piece p = pb.DrawTile();
            while (p != null)
            {
                corSet = b.GetValidSquares(p);
                if (corSet != null && corSet.Count > 0)
                {
                    c = corSet[r.Next(0, corSet.Count)];
                    b.AddPiece(c.X, c.Y, p);
                    moveCount++;
                    b.PrintFancyBoard();
                    b.EndTurn();
                }
                p = pb.DrawTile();
            }
            sw.Stop();
            Console.WriteLine("Program Completed in {0} ms", sw.ElapsedMilliseconds);
            Console.WriteLine("\tSystem completed {0} moves", moveCount);
            Console.WriteLine("\tAverage time per move was {0}ms", (float)sw.ElapsedMilliseconds / moveCount);
            Console.Read();
        }
    }
}
