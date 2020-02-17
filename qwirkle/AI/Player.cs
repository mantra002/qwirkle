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
        protected List<Tile> Hand { get; set; }
        public int Score { get; set; }

        public Player(TileBag bagToDrawFrom)
        {
            this.Hand = new List<Tile>();
            this.DrawTiles(bagToDrawFrom, Rules.HandSize);
            this.Score = 0;
        }

        public void DrawTiles(TileBag bagToDrawFrom, int numberOfTiles)
        {
            if (bagToDrawFrom == null) throw new Exception("Trying to draw from null piecebag!");
            if (bagToDrawFrom.BagCount < numberOfTiles)
            {
                numberOfTiles = bagToDrawFrom.BagCount;
            }
            for(int i = 0; i < numberOfTiles; i++)
            {
                this.Hand.Add(bagToDrawFrom.DrawTile());
            }
        }
        public void PlayTurn(Board currentGameBoard, TileBag bagToDrawFrom)
        {
            List<Move> bestMoves = this.findBestMoves(currentGameBoard);
            int tilesPlaced = 0;

            if (bestMoves.Count == 0 || bestMoves[0] == null)
            {
                this.Hand.Clear();
                this.DrawTiles(bagToDrawFrom, Rules.HandSize);
            }
            else
            {
                while (bestMoves.Count != 0 && bestMoves[0] != null)
                {
                    tilesPlaced += bestMoves.Count;

                    foreach (Move m in bestMoves)
                    {
                        currentGameBoard.AddTile(m.Location.X, m.Location.Y, m.Tile);
                        this.Hand.Remove(m.Tile);
                        //This is assuming the words formed by the final tile are the score. If their
                        //are words formed by intermediate tiles they will not be scored correctly.
                        this.Score += m.Score-1;
                    }
                    bestMoves = this.findBestMoves(currentGameBoard);
                }
                this.Score += tilesPlaced;
            }
           
            
            currentGameBoard.EndTurn();
            this.DrawTiles(bagToDrawFrom, tilesPlaced);
        }
        protected abstract List<Move> findBestMoves(Board currentGameBoard);
        
    }
}
