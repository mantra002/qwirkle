using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qwirkle.Game;

namespace Qwirkle.AI
{
    public abstract class Player
    {
        List<Piece> Hand { get; set; }
        int Score { get; set; }

        public Player()
        {
            this.Hand = new List<Piece>();
            this.Score = 0;
        }

        public void DrawTiles(int numberOfTiles, ref PieceBag bagToDrawFrom)
        {
            if (bagToDrawFrom == null) throw new Exception("Trying to draw from null piecebag!");
            for(int i =0; i < numberOfTiles; i++)
            {
                this.Hand.Add(bagToDrawFrom.DrawTile());
            }
        }

        private abstract Move findBestMove(ref Board currentGameBoard);
        public abstract void PlayTurn(ref Board currentGameBoard);
    }
}
