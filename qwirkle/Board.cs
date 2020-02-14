using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qwirkle
{
    public class Board
    {
        private List<List<int>> gameBoard = new List<List<int>>();
        private List<List<int>> previousGameBoard = new List<List<int>>();

        private int sizeX = 0;
        private int sizeY = 0;
        private Coord lastPiecePlaced;
        private bool newBoard = true;

        public int SizeX { get => sizeX; set => sizeX = value; }
        public int SizeY { get => sizeY; set => sizeY = value; }


        private enum Direction
        {
            North,
            East,
            South,
            West
        }


        public Board()
        {
            this.SizeX = Rules.InitialBoardSize;
            this.SizeY = Rules.InitialBoardSize;
            for (int x = 0; x < this.SizeX; x++)
            {
                this.gameBoard.Add(new List<int>(this.SizeY));
                for (int y = 0; y < this.SizeY; y++)
                {
                    this.gameBoard[x].Add(0);
                }
            }
            this.previousGameBoard = this.cloneBoard(gameBoard);
        }

        public int EndTurn()
        {
            this.previousGameBoard = this.cloneBoard(gameBoard);
            this.lastPiecePlaced = null;
            return 0;
        }

        public void ResetTurn()
        {
            this.lastPiecePlaced = null;
            this.gameBoard = this.cloneBoard(this.previousGameBoard);
        }

        private List<Coord> GetPossibleOpenSquares()
        {
            List<Coord> possibleSquares = new List<Coord>();
            int x, y;
            if (this.newBoard)
            {
                possibleSquares.Add(new Coord(this.sizeX / 2, this.sizeY / 2));
                return possibleSquares;
            }
            if (this.lastPiecePlaced == null)
            {
                for (x = 1; x < this.SizeX - 1; x++)
                {
                    for (y = 1; y < this.SizeY - 1; y++)
                    {
                        if (SquareEmptyWithAdjacentPieces(x, y))
                        {
                            possibleSquares.Add(new Coord(x, y));
                        }

                    }
                }
            }
            else
            {
                x = lastPiecePlaced.X;
                y = lastPiecePlaced.Y;
                if (SquareEmptyWithAdjacentPieces(x - 1, y))
                {
                    possibleSquares.Add(new Coord(x - 1, y));
                }
                if (SquareEmptyWithAdjacentPieces(x + 1, y))
                {
                    possibleSquares.Add(new Coord(x + 1, y));
                }
                if (SquareEmptyWithAdjacentPieces(x, y - 1))
                {
                    possibleSquares.Add(new Coord(x, y - 1));
                }
                if (SquareEmptyWithAdjacentPieces(x, y + 1))
                {
                    possibleSquares.Add(new Coord(x, y + 1));
                }
            }
            return possibleSquares;
        }

        private bool SquareEmptyWithAdjacentPieces(int x, int y)
        {
            return (this.gameBoard[x][y] == 0 && (
                            this.gameBoard[x + 1][y] != 0 ||
                            this.gameBoard[x][y + 1] != 0 ||
                            this.gameBoard[x - 1][y] != 0 ||
                            this.gameBoard[x][y - 1] != 0
                            ));
        }

        public List<Coord> GetValidSquares(Piece p)
        {
            List<Coord> possibleSquares = GetPossibleOpenSquares();
            List<Coord> validMoves = new List<Coord>();
            if (possibleSquares == null || possibleSquares.Count == 0)
            {
                return null;
            }
            if (this.newBoard)
            {
                validMoves.Add(new Coord(this.sizeX / 2, this.sizeY / 2));
                return validMoves;
            }

            foreach (Coord c in possibleSquares)
            {
                if (
                    (this.gameBoard[c.X + 1][c.Y] / 6 == (int)p.Shape ||
                    this.gameBoard[c.X + 1][c.Y] % 6 == (int)p.Color ||
                    this.gameBoard[c.X + 1][c.Y] == 0) &&
                    (this.gameBoard[c.X][c.Y + 1] / 6 == (int)p.Shape ||
                    this.gameBoard[c.X][c.Y + 1] % 6 == (int)p.Color ||
                    this.gameBoard[c.X][c.Y + 1] == 0) &&
                    (this.gameBoard[c.X - 1][c.Y] / 6 == (int)p.Shape ||
                    this.gameBoard[c.X - 1][c.Y] % 6 == (int)p.Color ||
                    this.gameBoard[c.X - 1][c.Y] == 0) &&
                    (this.gameBoard[c.X][c.Y - 1] / 6 == (int)p.Shape ||
                    this.gameBoard[c.X][c.Y - 1] % 6 == (int)p.Color ||
                    this.gameBoard[c.X][c.Y - 1] == 0)
                    )
                {
                    if(this.WordIsValid(c, p, WordInfo.Orientation.Horizontal) && this.WordIsValid(c, p, WordInfo.Orientation.Vertical))
                    {
                        validMoves.Add(c);
                    }
                }
            }
            return validMoves;
        }
        private bool WordIsValid(Coord coord, Piece piece, WordInfo.Orientation orient)
        {
            bool result = true;
            Coord startOfWord, endOfWord;
            Direction d1, d2;

            if (orient == WordInfo.Orientation.Horizontal)
            {
                d1 = Direction.East;
                d2 = Direction.West;
            }
            else //Vertical
            {
                d1 = Direction.North;
                d2 = Direction.South;
            }
            WordInfo wi = new WordInfo(null, null, orient);
            startOfWord = TraverseWordAndReturnIndex(coord, piece.Color, piece.Shape, d1, ref wi);
            endOfWord = TraverseWordAndReturnIndex(coord, piece.Color, piece.Shape, d2, ref wi);

            wi.StartPosition = startOfWord;
            wi.EndPosition = endOfWord;
            wi.Orient = orient;
            wi.PiecesInWord.Add(piece);

            return wi.ValidateWord();
        }

        private Coord TraverseWordAndReturnIndex(Coord startPosition, Rules.Color color, Rules.Shape shape, Direction dir, ref WordInfo wi)
        {
            int index = 0, nextX, nextY;
            Piece p;

            if (wi == null)
            {
                throw new Exception("Null word info passed to traverse!");
            }

            switch (dir)
            {
                case Direction.North:
                    nextX = startPosition.X;
                    nextY = startPosition.Y - 1;
                    break;
                case Direction.South:
                    nextX = startPosition.X;
                    nextY = startPosition.Y + 1;
                    break;
                case Direction.West:
                    nextX = startPosition.X + 1;
                    nextY = startPosition.Y;
                    break;
                case Direction.East:
                    nextX = startPosition.X - 1;
                    nextY = startPosition.Y;
                    break;
                default:
                    throw new Exception("Invalid direction");
            }
            if (this.gameBoard[nextX][nextY] != 0)
            {
                p = new Piece(this.gameBoard[nextX][nextY]);
                if (p != null && p.Shape == shape && (wi.WordType == WordInfo.TypeOfWord.ShapeMatch || wi.WordType == WordInfo.TypeOfWord.Unknown))
                {
                    wi.WordType = WordInfo.TypeOfWord.ShapeMatch;
                    wi.PiecesInWord.Add(p);
                    return TraverseWordAndReturnIndex(new Coord(nextX, nextY), color, shape, dir, ref wi);

                }
                else if (p != null && p.Color == color && (wi.WordType == WordInfo.TypeOfWord.ColorMatch || wi.WordType == WordInfo.TypeOfWord.Unknown))
                {
                    wi.WordType = WordInfo.TypeOfWord.ColorMatch;
                    wi.PiecesInWord.Add(p);
                    return TraverseWordAndReturnIndex(new Coord(nextX, nextY), color, shape, dir, ref wi);
                }
            }

            return new Coord(nextX, nextY);


        } 

        public void PrintFancyBoard()
        {
            Piece p;
            for (int x = 0; x < this.SizeX; x++)
            {
                for (int y = 0; y < this.SizeY; y++)
                {
                    if (gameBoard[x][y] == 0)
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        new Piece(gameBoard[x][y]).PrintFancyCharacter();
                    }
                }
                Console.WriteLine();
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int x = 0; x < this.SizeX; x++)
            {
                for (int y = 0; y < this.SizeY; y++)
                {
                    sb.Append(this.gameBoard[x][y].ToString("D2") + " ");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public void AddPiece(int X, int Y, Piece p)
        {
            if (p != null)
            {
                if (this.newBoard)
                {
                this.newBoard = false;
                }

                this.lastPiecePlaced = new Coord(X, Y);

                gameBoard[X][Y] = p.ToIndex();
                if (X < 2)
                {
                    this.addColumn();
                }
                else if (X > this.SizeX - 3)
                {
                    //Add column on right
                    this.addColumn(true);
                }
                if (Y < 2)
                {
                    this.addRow();
                    //Add row on top
                }
                else if (Y > this.SizeY - 3)
                {
                    this.addRow(true);
                    //Add row on bottom
                }
            }
        }

        private void addColumn(bool highSide = false)
        {
            this.SizeX++;
            if (highSide)
            {
                this.gameBoard.Add(new List<int>(this.SizeY));
                for (int y = 0; y < this.SizeY; y++)
                {
                    this.gameBoard[this.SizeX-1].Add(0);
                }
            }
            else
            {
                this.gameBoard.Insert(0, new List<int>(this.SizeY));
                for (int y = 0; y < this.SizeY; y++)
                {
                    this.gameBoard[0].Add(0);
                }
            }
        }

        private void addRow(bool highSide = false)
        {
            this.SizeY++;
            if (highSide)
            {
                for(int x = 0; x < this.SizeX; x++)
                {
                    this.gameBoard[x].Add(0);
                }
            }
            else
            {
                for (int x = 0; x < this.SizeX; x++)
                {
                    this.gameBoard[x].Insert(0, 0);
                }
            }
        }

        private List<List<int>> cloneBoard(List<List<int>> board)
        {
            List<List<int>> clonedBoard = new List<List<int>>();

            for (int x = 0; x < this.SizeX; x++)
            {
                clonedBoard.Add(new List<int>());
                for (int y = 0; y < this.SizeY; y++)
                {
                    clonedBoard[x].Add(board[x][y]);
                }
            }
            return clonedBoard;
        }

    }

}
