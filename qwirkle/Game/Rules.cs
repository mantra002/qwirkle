using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qwirkle.Game
{
    public static class Rules
    {
        public const int NumberOfDuplicateTiles = 3;
        public const int InitialBoardSize = 5;
        public const int HandSize = 6;
        public enum Color
        {
            Red,
            Orange,
            Purple,
            Blue,
            Yellow,
            Green
        }

        public enum Shape
        {
            Circle,
            Cross,
            Diamond,
            Square,
            Star,
            X
        }
    }
}
