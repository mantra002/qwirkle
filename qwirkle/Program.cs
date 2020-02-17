using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Qwirkle.Game;
using Qwirkle.AI;

namespace Qwirkle
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int greedTotal = 0, randTotal = 0;
            int reps = 1;
            int moveCount = 0;
            for (int i = 0; i < reps; i++)
            {
                TileBag tb = new TileBag();
                Board b = new Board();
                Debug.AutoFlush = true;
                GreedyPlayer greedyPlayer = new GreedyPlayer(tb);
                RandomPlayer randomPlayer = new RandomPlayer(tb);

                while (tb.BagCount > 0)
                {
                    greedyPlayer.PlayTurn(b, tb);
                    b.PrintFancyBoard();
                    moveCount++;
                    randomPlayer.PlayTurn(b, tb);
                    b.PrintFancyBoard();
                    moveCount++;
                }
                greedTotal += greedyPlayer.Score;
                randTotal += randomPlayer.Score;

            }
            Console.WriteLine("Final Greedy Score was {0}", greedTotal/(double) reps);
            Console.WriteLine("Final Random Score was {0}", randTotal / (double)reps);
            sw.Stop();
            Console.WriteLine("Program Completed in {0} ms", sw.ElapsedMilliseconds);
            Console.WriteLine("\tSystem completed {0} turns", moveCount);
            Console.WriteLine("\tAverage time per turn was {0}ms", (float)sw.ElapsedMilliseconds / moveCount);
            
            Console.Read();
        }
    }
}
