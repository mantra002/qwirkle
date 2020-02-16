using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qwirkle.Game
{
    public class PieceBag
    {
        List<Piece> bag = new List<Piece>();
        
        public PieceBag()
        {
            this.FillBagWithPieces();
        }

        public void FillBagWithPieces(bool emptyBagFirst = true)
        {
            if(emptyBagFirst) this.bag.Clear();
            for(int i = 0; i < Rules.NumberOfDuplicatePieces; i++)
            {
                foreach(Rules.Color color in Enum.GetValues(typeof(Rules.Color)))
                {
                    foreach (Rules.Shape shape in Enum.GetValues(typeof(Rules.Shape)))
                    {
                        bag.Add(new Piece(color, shape));
                    }
                }
            }
            bag.Shuffle();
        }


        public int BagCount
        {
            get { return this.bag.Count; }
        }

        public Piece DrawTile()
        {
            if (bag.Count == 0)
            {
                return null;
            }
            Piece p = bag[0];
            bag.RemoveAt(0);
            return p;
        }
    }
}
