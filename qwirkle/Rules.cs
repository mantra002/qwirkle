using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qwirkle
{
    public static class Rules
    {
        public const int NumberOfDuplicatePieces = 3;
        public const int InitialBoardSize = 5;

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
