using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Qwirkle
{
    public class WordInfo
    {
        public enum TypeOfWord
        {
            ShapeMatch,
            ColorMatch,
            Unknown
        }
        public enum Orientation
        {
            Vertical,
            Horizontal
        }


        private Coord startPosition;
        private Coord endPosition;
        private TypeOfWord wordType;
        private Orientation orient;

        public Coord EndPosition { get => endPosition; set => endPosition = value; }
        public Coord StartPosition { get => startPosition; set => startPosition = value; }
        public TypeOfWord WordType { get => wordType; set => wordType = value; }
        public Orientation Orient { get => orient; set => orient = value; }
        public List<Piece> PiecesInWord = new List<Piece>();

        public WordInfo(Coord startPosition, Coord endPosition, Orientation o)
        {
            this.startPosition = startPosition;
            this.endPosition = endPosition;
            this.orient = o;
            this.wordType = TypeOfWord.Unknown;
        }
        public bool ValidateWord()
        {
            bool valid = true;
            if (this.PiecesInWord.Count > 6 || this.PiecesInWord.Count == 0) return false;
            if(this.wordType == TypeOfWord.ColorMatch)
            {
                Rules.Color baseColor = this.PiecesInWord[0].Color;
                List<Rules.Shape> usedShapes = new List<Rules.Shape>();
                foreach (Piece p in this.PiecesInWord)
                {
                    if (p.Color != baseColor)
                    {
                        valid = false;
                    }
                    else if (usedShapes.Contains(p.Shape))
                    {
                        valid = false;
                    }
                    else
                    {
                        usedShapes.Add(p.Shape);
                    }
                }
            }
            else
            {
                Rules.Shape baseShape = this.PiecesInWord[0].Shape;
                List<Rules.Color> usedColors = new List<Rules.Color>();
                foreach (Piece p in this.PiecesInWord)
                {
                    if (p.Shape != baseShape)
                    {
                        valid = false;
                    }
                    else if (usedColors.Contains(p.Color))
                    {
                        valid = false;
                    }
                    else
                    {
                        usedColors.Add(p.Color);
                    }
                }
            }
            if (valid) Debug.WriteLine("Word IS valid.");
            else Debug.WriteLine("Word is NOT valid.");
            return valid;
        }
    }
}
