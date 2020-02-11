using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qwirkle
{
    public class PieceBag
    {
        List<Piece> bag = new List<Piece>();
        
        public PieceBag()
        {

        }

        public void FillBagWithPieces(bool emptyBagFirst = true)
        {
            if(emptyBagFirst) this.bag.Clear();
            for(int i = 0; i < Rules.NUMBER_OF_DUPLICATE_PIECES; i++)
            {
                foreach(Rules.Colors color in Enum.GetValues(typeof(Rules.Colors)))
                {
                    foreach (Rules.Shapes shape in Enum.GetValues(typeof(Rules.Shapes)))
                    {
                        bag.Add(new Piece(color, shape));
                    }
                }
            }
            bag.Shuffle();
        }

        public Piece DrawTile()
        {
            Piece p = bag.Take(1);

            return p;
        }
    }
}
