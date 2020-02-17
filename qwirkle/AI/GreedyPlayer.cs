using Qwirkle.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qwirkle.AI
{
    public class GreedyPlayer : Player
    {
        public GreedyPlayer(TileBag bagToDrawFrom) : base(bagToDrawFrom) { }

        protected override List<Move> findBestMoves(Board currentGameBoard)
        {
            List<Move> moves = new List<Move>();
            Move highestScore = null;
            foreach(Tile t in this.Hand)
            {
                List<Move> moveSet = currentGameBoard.GetValidSquares(t);
                if(moveSet == null) return new List<Move> { null };
                foreach (Move m in moveSet)
                {
                    if (highestScore == null) highestScore = m;
                    else
                    {
                        if (highestScore.Score < m.Score) highestScore = m;
                    }
                }
            }
            return new List<Move> { highestScore };
        }
    }
}
