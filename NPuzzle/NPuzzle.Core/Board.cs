using System;
using System.Diagnostics.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Amv.NPuzzle.Core
{
    public class Board
    {
        private readonly short[,] _cells;

        /// <summary>
        /// Construct a board from an n-by-n array of blocks (where blocks[i][j] = block in row i, column j)
        /// </summary>
        /// <param name="blocks"></param>
        public Board(int[][] blocks)
        {
            Contract.Requires(blocks != null);
            Contract.Requires(blocks.All(line => line != null));
            Contract.Requires(blocks.All(line => line.Length == blocks.Length));

            int n = blocks.Length;
            _cells = new short[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    _cells[i, j] = (short)blocks[i][j];
                }
            }
        }

        public Board(short[,] blocks)
        {
            Contract.Requires(blocks != null);
            Contract.Requires(blocks.GetLength(0) == blocks.GetLength(1));

            _cells = blocks;
        }

        /// <summary>
        /// Board dimension N
        /// </summary>
        public short Dimension => (short)_cells.GetLength(0);

        /// <summary>
        /// All neighboring boards
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Board> GetNeighbors()
        {
            var n = Dimension;
            short i = 0, j = 0;
            for (; i < n; i++)
            {
                j = 0;
                for (; j < n; j++)
                {
                    if (_cells[i, j] == 0)
                    {
                        goto exit;
                    }
                }
            }
            exit:

            if (i > 0)
            {
                var newCells = (short[,]) _cells.Clone();
                newCells[i, j] = newCells[i - 1, j];
                newCells[i - 1, j] = 0;
                yield return new Board(newCells);
            }
            if (i < n-1)
            {
                var newCells = (short[,])_cells.Clone();
                newCells[i, j] = newCells[i + 1, j];
                newCells[i + 1, j] = 0;
                yield return new Board(newCells);
            }
            if (j > 0)
            {
                var newCells = (short[,])_cells.Clone();
                newCells[i, j] = newCells[i, j - 1];
                newCells[i, j - 1] = 0;
                yield return new Board(newCells);
            }
            if (j < n-1)
            {
                var newCells = (short[,])_cells.Clone();
                newCells[i, j] = newCells[i, j + 1];
                newCells[i, j + 1] = 0;
                yield return new Board(newCells);
            }
        }

        public short[,] Cells => _cells;

        public override string ToString()
        {
            var sb = new StringBuilder();
            int n = Dimension;

            int padding = (n * n - 1).ToString().Length;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    sb.Append(_cells[i, j].ToString().PadLeft(padding));
                    if (j < n - 1)
                    {
                        sb.Append(' ');
                    }
                }
                if (i < n - 1)
                {
                    sb.AppendLine();
                }
            }
            return sb.ToString();
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return Equals((Board)obj);
        }

        public bool Equals(Board target)
        {
            if (target == null)
                return false;

            int n = Dimension;
            if (n == target.Dimension)
            {
                int res = 0;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (_cells[i, j] != target._cells[i, j])
                        {
                            break;
                        }
                        res++;
                    }
                }
                return res == n * n;
            }
            return false;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            unchecked
            {
                if (_cells == null)
                {
                    return 0;
                }
                int hash = 17;
                foreach (short element in _cells)
                {
                    hash = hash * 31 + element;
                }
                return hash;
            }
        }

        public (short Row, short Col)[] GetMap()
        {
            var n = Dimension;
            var result = new ValueTuple<short, short>[n * n];
            for (short i = 0; i < n; i++)
            {
                for (short j = 0; j < n; j++)
                {
                    result[_cells[i,j]] = (i, j);
                }
            }
            return result;
        }

        public static Board GetClassicalGoal(short dimention)
        {
            var cells = new short[dimention, dimention];

            for (short i = 0; i < dimention; i++)
            {
                for (short j = 0; j < dimention; j++)
                {
                    cells[i , j] = (short)(i*dimention + j + 1) ;
                }
            }

            cells[dimention - 1, dimention - 1] = 0;
            return new Board(cells);
        }
    }
}
