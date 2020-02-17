using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qwirkle.Game
{
    public class TileBag
    {
        List<Tile> bag = new List<Tile>();
        
        public TileBag()
        {
            this.FillBagWithTiles();
        }

        public void FillBagWithTiles(bool emptyBagFirst = true)
        {
            if(emptyBagFirst) this.bag.Clear();
            for(int i = 0; i < Rules.NumberOfDuplicateTiles; i++)
            {
                foreach(Rules.Color color in Enum.GetValues(typeof(Rules.Color)))
                {
                    foreach (Rules.Shape shape in Enum.GetValues(typeof(Rules.Shape)))
                    {
                        bag.Add(new Tile(color, shape));
                    }
                }
            }
            bag.Shuffle();
        }


        public int BagCount
        {
            get { return this.bag.Count; }
        }

        public Tile DrawTile()
        {
            if (bag.Count == 0)
            {
                return null;
            }
            Tile p = bag[0];
            bag.RemoveAt(0);
            return p;
        }
    }
}
