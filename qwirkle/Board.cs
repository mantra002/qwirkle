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
        private bool newBoard = true;

        public int SizeX { get => sizeX; set => sizeX = value; }
        public int SizeY { get => sizeY; set => sizeY = value; }

        public Board()
        {
            this.SizeX = Rules.InitialBoardSize;
            this.SizeY = Rules.InitialBoardSize;
            for (int x = 0; x < this.SizeX; x++)
            {
                this.gameBoard.Add(new List<int>(this.SizeY));
                for(int y = 0; y < this.SizeY; y++)
                {
                    this.gameBoard[x].Add(0);
                }
            }
            this.previousGameBoard = this.cloneBoard(gameBoard);
        }
        
        public int EndTurn()
        {
            this.previousGameBoard = this.cloneBoard(gameBoard);
            return 0;
        }

        public void ResetTurn()
        {
            this.gameBoard = this.cloneBoard(this.previousGameBoard);
        }

        public List<Coords> GetPossibleOpenSquares()
        {
            List<Coords> possibleSquares = new List<Coords>();
            if(this.newBoard)
            {
                possibleSquares.Add(new Coords(this.sizeX / 2, this.sizeY / 2));
                return possibleSquares;
            }
            for (int x = 1; x < this.SizeX-1; x++)
            {
                for (int y = 1; y < this.SizeY-1; y++)
                {
                    if(this.gameBoard[x][y] == 0 && (
                        this.gameBoard[x+1][y] != 0 ||
                        this.gameBoard[x][y+1] != 0 ||
                        this.gameBoard[x-1][y] != 0 ||
                        this.gameBoard[x][y-1] != 0
                        ))
                    {
                        possibleSquares.Add(new Coords(x, y));
                    }
                        
                }
            }
            return possibleSquares;
        }

        public List<Coords> GetValidSquares(List<Coords> possibleSquares, Piece p)
        {
            List<Coords> validMoves = new List<Coords>();
            if (possibleSquares == null || possibleSquares.Count == 0)
            {
                return null;
            }
            if (this.newBoard)
            {
                validMoves.Add(new Coords(this.sizeX / 2, this.sizeY / 2));
                return validMoves;
            }

            foreach(Coords c in possibleSquares)
            {
                if (
                    (this.gameBoard[c.x + 1][c.y] / 6 == (int)p.Shape ||
                    this.gameBoard[c.x + 1][c.y] % 6 == (int)p.Color ||
                    this.gameBoard[c.x + 1][c.y] == 0) &&
                    (this.gameBoard[c.x][c.y+1] / 6 == (int)p.Shape ||
                    this.gameBoard[c.x][c.y + 1] % 6 == (int)p.Color ||
                    this.gameBoard[c.x][c.y + 1] == 0) &&
                    (this.gameBoard[c.x - 1][c.y] / 6 == (int)p.Shape ||
                    this.gameBoard[c.x - 1][c.y] % 6 == (int)p.Color ||
                    this.gameBoard[c.x - 1][c.y] == 0) &&
                    (this.gameBoard[c.x][c.y - 1] / 6 == (int)p.Shape ||
                    this.gameBoard[c.x][c.y - 1] % 6 == (int)p.Color ||
                    this.gameBoard[c.x][c.y - 1] == 0)
                    )
                {
                    validMoves.Add(c);
                }
            }
            return validMoves;
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
    public struct Coords
    {
        public int x, y;

        public Coords(int p1, int p2)
        {
            x = p1;
            y = p2;
        }
    }
}
