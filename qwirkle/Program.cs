using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Qwirkle.Game;

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
            List<Move> moveSet;
            Move m;
            int score = 0;
            //Debug.AutoFlush = true;
            b.PrintFancyBoard();
            Piece p = pb.DrawTile();
            while (p != null)
            {
                moveSet = b.GetValidSquares(p);
                if (moveSet != null && moveSet.Count > 0)
                {
                    m = moveSet[r.Next(0, moveSet.Count)];
                    b.AddPiece(m.Location.X, m.Location.Y, m.Piece);
                    score += m.Score;
                    moveCount++;
                    b.PrintFancyBoard();
                    b.EndTurn();
                }
                p = pb.DrawTile();
            }
            Console.WriteLine("Final Score was {0}", score);
            sw.Stop();
            Console.WriteLine("Program Completed in {0} ms", sw.ElapsedMilliseconds);
            Console.WriteLine("\tSystem completed {0} moves", moveCount);
            Console.WriteLine("\tAverage time per move was {0}ms", (float)sw.ElapsedMilliseconds / moveCount);
            Console.Read();
        }
    }
}
