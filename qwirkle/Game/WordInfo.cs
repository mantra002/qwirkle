using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Qwirkle.Game
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
        public List<Piece> PiecesInWord;

        public WordInfo(Coord startPosition, Coord endPosition, Orientation o, TypeOfWord tow = TypeOfWord.Unknown)
        {
            this.startPosition = startPosition;
            this.endPosition = endPosition;
            this.orient = o;
            this.wordType = tow;
            this.PiecesInWord = new List<Piece>();
        }
        public int ValidateWordAndReturnScore()
        {
            int score = 1;
            if (this.PiecesInWord.Count > 6 || this.PiecesInWord.Count == 0) return -1;
            if(this.WordType == TypeOfWord.Unknown && this.PiecesInWord.Count == 1)
            {
                //Do nothing
            }
            else if(this.wordType == TypeOfWord.ColorMatch)
            {
                Rules.Color baseColor = this.PiecesInWord[0].Color;
                List<Rules.Shape> usedShapes = new List<Rules.Shape>();
                foreach (Piece p in this.PiecesInWord)
                {
                    if (p.Color == baseColor && !usedShapes.Contains(p.Shape))
                    {
                        usedShapes.Add(p.Shape);
                    }
                    else
                    {
                        score = -1;
                    }
                }
            }
            else if(this.wordType == TypeOfWord.ShapeMatch)
            {
                Rules.Shape baseShape = this.PiecesInWord[0].Shape;
                List<Rules.Color> usedColors = new List<Rules.Color>();
                foreach (Piece p in this.PiecesInWord)
                {
                    if (p.Shape == baseShape && !usedColors.Contains(p.Color))
                    {
                        usedColors.Add(p.Color);
                    }
                    else
                    {
                        score = -1;
                    }
                }
            }
            else
            {
                throw new Exception("Invalid word paramters.");
            }
            if (score > 0)
            {
                if(this.PiecesInWord.Count == 6)
                {
                    score = 12;
                }
                else
                {
                    score = this.PiecesInWord.Count;
                }
                Debug.WriteLine(this.Orient.ToString() + " Word IS valid.");
            }
            else Debug.WriteLine(this.Orient.ToString() + " Word is NOT valid.");
            return score;
        }
    }
}
