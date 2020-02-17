using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Qwirkle.Game
{
    public class Board
    {
        private List<List<int>> gameBoard = new List<List<int>>();
        private List<List<int>> previousGameBoard = new List<List<int>>();

        private int sizeX = 0;
        private int sizeY = 0;
        private Coord lastTilePlaced;
        private BoardState boardStatus;

        public int SizeX { get => sizeX; set => sizeX = value; }
        public int SizeY { get => sizeY; set => sizeY = value; }

        private enum BoardState
        {
            NewBoard,
            NewBoardInProgress,
            InProgress
        }

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
            this.boardStatus = BoardState.NewBoard;
        }

        public int EndTurn()
        {
            this.previousGameBoard = this.cloneBoard(gameBoard);
            this.lastTilePlaced = null;
            this.boardStatus = BoardState.InProgress;
            return 0;
        }

        public void ResetTurn()
        {
            if (this.boardStatus == BoardState.NewBoardInProgress) this.boardStatus = BoardState.NewBoard;
            this.lastTilePlaced = null;
            this.gameBoard = this.cloneBoard(this.previousGameBoard);
        }

        private List<Coord> GetPossibleOpenSquares()
        {
            List<Coord> possibleSquares = new List<Coord>();
            int x, y;
            if (this.boardStatus == BoardState.NewBoard)
            {
                possibleSquares.Add(new Coord(this.sizeX / 2, this.sizeY / 2));
                return possibleSquares;
            }
            if (this.lastTilePlaced == null)
            {
                for (x = 1; x < this.SizeX - 1; x++)
                {
                    for (y = 1; y < this.SizeY - 1; y++)
                    {
                        if (SquareEmptyWithAdjacentTiles(x, y))
                        {
                            possibleSquares.Add(new Coord(x, y));
                        }

                    }
                }
            }
            else
            {
                x = lastTilePlaced.X;
                y = lastTilePlaced.Y;
                if (SquareEmptyWithAdjacentTiles(x - 1, y))
                {
                    possibleSquares.Add(new Coord(x - 1, y));
                }
                if (SquareEmptyWithAdjacentTiles(x + 1, y))
                {
                    possibleSquares.Add(new Coord(x + 1, y));
                }
                if (SquareEmptyWithAdjacentTiles(x, y - 1))
                {
                    possibleSquares.Add(new Coord(x, y - 1));
                }
                if (SquareEmptyWithAdjacentTiles(x, y + 1))
                {
                    possibleSquares.Add(new Coord(x, y + 1));
                }
            }
            return possibleSquares;
        }

        private bool SquareEmptyWithAdjacentTiles(int x, int y)
        {
            return (this.gameBoard[x][y] == 0 && (
                            this.gameBoard[x + 1][y] != 0 ||
                            this.gameBoard[x][y + 1] != 0 ||
                            this.gameBoard[x - 1][y] != 0 ||
                            this.gameBoard[x][y - 1] != 0
                            ));
        }

        public List<Move> GetValidSquares(Tile p)
        {
            List<Coord> possibleSquares = GetPossibleOpenSquares();
            List<Move> validMoves = new List<Move>();
            if (possibleSquares == null || possibleSquares.Count == 0 || p == null)
            {
                return null;
            }
            if (this.boardStatus == BoardState.NewBoard)
            {
                validMoves.Add(new Move(p, new Coord(this.sizeX / 2, this.sizeY / 2), 1));
                return validMoves;
            }

            foreach (Coord c in possibleSquares)
            {
                if (
                    (
                    this.gameBoard[c.X + 1][c.Y]  / 6  == (int)p.Shape ||
                    this.gameBoard[c.X + 1][c.Y]  % 6  == (int)p.Color ||
                    this.gameBoard[c.X + 1][c.Y] == 0) &&
                    (
                    this.gameBoard[c.X][c.Y + 1]  / 6  == (int)p.Shape ||
                    this.gameBoard[c.X][c.Y + 1]  % 6  == (int)p.Color ||
                    this.gameBoard[c.X][c.Y + 1] == 0) &&
                    (
                    this.gameBoard[c.X - 1][c.Y]  / 6  == (int)p.Shape ||
                    this.gameBoard[c.X - 1][c.Y]  % 6  == (int)p.Color ||
                    this.gameBoard[c.X - 1][c.Y] == 0) &&
                    (
                    this.gameBoard[c.X][c.Y - 1]  / 6  == (int)p.Shape ||
                    this.gameBoard[c.X][c.Y - 1]  % 6  == (int)p.Color ||
                    this.gameBoard[c.X][c.Y - 1]       == 0)
                    )
                {
                    int score = this.CheckIfWordIsValidAndScore(c, p);
                    if (score > 0)
                    {
                        validMoves.Add(new Move(p, c, score));
                    }
                }
            }
#if DEBUG
            Debug.Print("Current Valid Moves for " + p.ToString());
            Debug.Indent();
            foreach (Move m in validMoves)
            {
                Debug.Print(m.Location.ToString());
            }
            Debug.Print("");
            Debug.Unindent();
#endif
            return validMoves;
        }
        private int CheckIfWordIsValidAndScore(Coord coord, Tile tile)
        {
            int vertScore = 0, horiScore = 0;
            Coord startOfWord, endOfWord, startOfWord2, endOfWord2;
            Debug.WriteLine("Trying to add " + tile.ToString() + " at " + coord.ToString());
            Debug.Indent();
            WordInfo wi = new WordInfo(null, null, WordInfo.Orientation.Horizontal);
            Debug.WriteLine("Starting Word Traverse West to East...");

            startOfWord = TraverseWordAndReturnIndex(coord, tile.Color, tile.Shape, Direction.West, ref wi);
            Debug.Indent();
            Debug.WriteLine("Found word start at " + startOfWord.ToString());
            endOfWord = TraverseWordAndReturnIndex(coord, tile.Color, tile.Shape, Direction.East, ref wi);
            Debug.WriteLine("Word is of type " + wi.WordType);
            Debug.WriteLine("Found word end at " + endOfWord.ToString());
            Debug.Unindent();
            wi.StartPosition = startOfWord;
            wi.EndPosition = endOfWord;
            wi.TilesInWord.Add(tile);

            WordInfo wi2 = new WordInfo(null, null, WordInfo.Orientation.Vertical, wi.WordType);
            Debug.WriteLine("\nStarting Word Traverse North to South...");
            Debug.Indent();
            startOfWord2 = TraverseWordAndReturnIndex(coord, tile.Color, tile.Shape, Direction.North, ref wi2);
            Debug.WriteLine("Found word start at " + startOfWord2.ToString());
            endOfWord2 = TraverseWordAndReturnIndex(coord, tile.Color, tile.Shape, Direction.South, ref wi2);
            Debug.WriteLine("Word is of type " + wi2.WordType);
            Debug.WriteLine("Found word end at " + endOfWord2.ToString());
            wi2.TilesInWord.Add(tile);
            Debug.Unindent();
            Debug.Unindent();
            vertScore = wi.ValidateWordAndReturnScore();
            horiScore = wi2.ValidateWordAndReturnScore();

            if (vertScore > 0 && horiScore > 0) return vertScore + horiScore;
            else return 0;
        }

        private Coord TraverseWordAndReturnIndex(Coord startPosition, Rules.Color color, Rules.Shape shape, Direction dir, ref WordInfo wi)
        {
            int nextX, nextY;
            Tile p;

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
                    nextX = startPosition.X - 1;
                    nextY = startPosition.Y;
                    break;
                case Direction.East:
                    nextX = startPosition.X + 1;
                    nextY = startPosition.Y;
                    break;
                default:
                    throw new Exception("Invalid direction");
            }
            if (this.gameBoard[nextX][nextY] != 0)
            {
                p = new Tile(this.gameBoard[nextX][nextY]);
                if(p != null)
                {
                    if (p.Shape == shape && (wi.WordType == WordInfo.TypeOfWord.ShapeMatch || wi.WordType == WordInfo.TypeOfWord.Unknown))
                    {
                        wi.WordType = WordInfo.TypeOfWord.ShapeMatch;
                    }
                    else if (p.Color == color && (wi.WordType == WordInfo.TypeOfWord.ColorMatch || wi.WordType == WordInfo.TypeOfWord.Unknown))
                    {
                        wi.WordType = WordInfo.TypeOfWord.ColorMatch;
                    }
                    wi.TilesInWord.Add(p);
                    return TraverseWordAndReturnIndex(new Coord(nextX, nextY), color, shape, dir, ref wi);
                }
                
            }

            return startPosition;


        } 

        public void PrintFancyBoard(bool printGridNumbers = true)
        {
            int padSize = Math.Max((int)Math.Floor(Math.Log10((double)this.sizeX) + 1), (int)Math.Floor(Math.Log10((double)this.sizeY) + 1))+1;
            if (printGridNumbers)
            {
                Console.Write("".PadLeft(padSize+1));
                for (int x = 0; x < this.SizeX; x++)
                {
                    Console.Write((x + " ").PadLeft(padSize));
                }
                Console.WriteLine();
            }
            for (int y = 0; y < this.SizeY; y++)
            {
                for (int x = 0; x < this.SizeX; x++)
                {
                
                    if(x == 0) Console.Write((y + " ").PadLeft(padSize));
                    if (gameBoard[x][y] == 0)
                    {
                        Console.Write("".PadLeft(padSize));
                    }
                    else
                    {
                        new Tile(gameBoard[x][y]).PrintFancyCharacter(padSize);
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

        public void AddTile(int X, int Y, Tile p)
        {
            if (p != null)
            {
                if (this.boardStatus == BoardState.NewBoard)
                {
                    this.boardStatus = BoardState.NewBoardInProgress;
                }

                this.lastTilePlaced = new Coord(X, Y);

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
                if (this.lastTilePlaced != null)
                {
                    this.lastTilePlaced.X += 1;
                }
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
                if(this.lastTilePlaced != null)
                {
                    this.lastTilePlaced.Y += 1;
                }
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
