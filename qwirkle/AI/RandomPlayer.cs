using Qwirkle.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qwirkle.AI
{
    public class RandomPlayer : Player
    {
        private Random r;
        public RandomPlayer(TileBag tb) : base(tb) 
        {
            r = new Random();
        }

        protected override List<Move> findBestMoves(Board currentGameBoard)
        {
            List<Move> moveSet = null;
            if (this.Hand.Count > 0)
            {
                for (int i = 0; i < this.Hand.Count; i++)
                {
                    moveSet = currentGameBoard.GetValidSquares(this.Hand[i]);
                    if(moveSet == null) return new List<Move> { null };
                    if (moveSet.Count != 0) break;
                }

                if (moveSet.Count > 0)
                {
                    int randomIndex = r.Next(0, moveSet.Count);
                    return new List<Move> { moveSet[randomIndex] };
                }
            }
            return new List<Move> { null };
        }
    }
}
