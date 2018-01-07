using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Amv.NPuzzle.Core
{
    public class PuzzleSolver
    {
        private readonly IHeurisiticCalculator _manhattenCalculator;
        private readonly IHeurisiticCalculator _hammingCalculator;
        private readonly IHeurisiticCalculator _liniarConflictsCalculator;
        private static readonly IHeurisiticCalculator InversionCalculator = new InversionCalculator();
        private static readonly IComparer<SolutionNode> SolutionNodeComparer = new SolutionNodeComparer();

        private readonly Board _initialBoard;
        private readonly (short Row, short Col)[] _targetBoardMap;
        private readonly Lazy<bool> _isSolvable;

        public PuzzleSolver(Board initialBoard, Board targetBoard)
        {
            Contract.Requires(initialBoard != null);
            Contract.Requires(targetBoard != null);

            _initialBoard = initialBoard;
            _targetBoardMap=targetBoard.GetMap();
            _manhattenCalculator = new ManhattenCalculator(_targetBoardMap);
            _hammingCalculator = new HammingCalculator(_targetBoardMap);
            _liniarConflictsCalculator = new LinearConflictCalculator(_targetBoardMap);

            _isSolvable = new Lazy<bool>(IsSolvableInternal);
        }

        public bool IsSolvable => _isSolvable.Value;

        private bool IsSolvableInternal()
        {
            short n = _initialBoard.Dimension;
            var inversions = InversionCalculator.Calculate(_initialBoard.Cells);

            if (_initialBoard.Dimension % 2 == 1)
            {
                return inversions % 2 == 0;
            }

            short zeroPos = 0;
            for (short i = (short)(n - 1); i >= 0; i--)
                for (short j = (short)(n - 1); j >= 0; j--)
                    if (_initialBoard.Cells[i,j] == 0)
                        zeroPos = (short)(n - i);

            return zeroPos%2 != inversions % 2;
        }

        public PuzzleResult Solve()
        {
            if(!IsSolvable)
                return PuzzleResult.Unsolvable;

            var queue = new PriorityQueue<SolutionNode>(11, SolutionNodeComparer);
            var initial = CreateNode(_initialBoard, 0);
            if (initial.IsGoal)
            {
                return GetResult(initial);
            }

            queue.Enqueue(initial);

            var result = ProcessQueue(queue);

            return result;
        }

        private PuzzleResult ProcessQueue(PriorityQueue<SolutionNode> queue)
        {
            int i = 0;
            PuzzleResult result = null;
            while (queue.Count > 0 && result == null && i < 15000000)
            {
                result = ProcessQueueItem(queue);

                i++;
            }

            return result;
        }

        private PuzzleResult ProcessQueueItem(PriorityQueue<SolutionNode> queue)
        {
            var node = queue.Dequeue();
            PuzzleResult result = null;
            foreach (var child in GetChildrenNodes(node))
            {
                if (child.IsGoal)
                {
                    result = GetResult(child);
                    break;
                }

                queue.Enqueue(child);
            }

            return result;
        }

        private SolutionNode CreateNode(Board board, int step)
        {
            return new SolutionNode(board, 
                _manhattenCalculator.Calculate(board.Cells)+_liniarConflictsCalculator.Calculate(board.Cells),
                _hammingCalculator.Calculate(board.Cells),
                step);
        }

        public IEnumerable<SolutionNode> GetChildrenNodes(SolutionNode source)
        {
            var parentBoard = source.Parent?.Board;
            bool found = false;
            foreach (var neighbor in source.Board.GetNeighbors())
            {
                if (!found && neighbor.Equals(parentBoard))
                {
                    found = true;
                    continue;
                }
                yield return new SolutionNode(neighbor,
                    _manhattenCalculator.Calculate(neighbor.Cells) + _liniarConflictsCalculator.Calculate(neighbor.Cells),
                    _hammingCalculator.Calculate(neighbor.Cells),
                    source.Step+1,
                    source);
            }
        }

        private PuzzleResult GetResult(SolutionNode node)
        {
            var moves = node.Step + 1;
            var boards = new Board[moves];
            SolutionNode currentNode = node;
            for (int i = 0; i < moves; i++)
            {
                boards[moves-i-1] = currentNode.Board;
                currentNode = currentNode.Parent;
            }

            return new PuzzleResult(Array.AsReadOnly(boards));
        }
    }
}
