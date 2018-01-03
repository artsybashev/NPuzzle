using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Amv.NPuzzle.Core
{
    public class PuzzleSolver
    {
        private static readonly IManhattenCalculator ManhattenCalculator = new ManhattenCalculator();
        private static readonly IHammingCalculator HammingCalculator = new HammingCalculator();
        private static readonly IComparer<SolutionNode> SolutionNodeComparer = new SolutionNodeComparer();

        private readonly Board _initialBoard;
        private (short Row, short Col)[] _targetBoardMap;

        public PuzzleSolver(Board initialBoard, Board targetBoard)
        {
            Contract.Requires(initialBoard != null);
            Contract.Requires(targetBoard != null);

            _initialBoard = initialBoard;
            _targetBoardMap=targetBoard.GetMap();

            var n =_initialBoard.Dimension;
        }

        public bool IsSolvable
        {
            get { return true; }
        }

        public PuzzleResult Solve()
        {
            var queue = new PriorityQueue<SolutionNode>(11, SolutionNodeComparer);
            var initial = CreateNode(_initialBoard, 0);
            queue.Enqueue(initial);

            var result = ProcessQueue(queue);

            return result;
        }

        private PuzzleResult ProcessQueue(PriorityQueue<SolutionNode> queue)
        {
            int i = 0;
            PuzzleResult result = null;
            while (queue.Count > 0 && result == null && i < 5000000)
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
                    result = GetResult(node);
                    break;
                }

                queue.Enqueue(child);
            }

            return result;
        }

        private SolutionNode CreateNode(Board board, int step)
        {
            return new SolutionNode(board, 
                ManhattenCalculator.GetManhattan(board.Cells, _targetBoardMap),
                HammingCalculator.Calculate(board.Cells, _targetBoardMap),
                0);
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
                    ManhattenCalculator.GetManhattan(neighbor.Cells, _targetBoardMap),
                    HammingCalculator.Calculate(neighbor.Cells, _targetBoardMap),
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
                boards[i] = currentNode.Board;
                currentNode = currentNode.Parent;
            }

            return new PuzzleResult(Array.AsReadOnly(boards));
        }
    }
}
