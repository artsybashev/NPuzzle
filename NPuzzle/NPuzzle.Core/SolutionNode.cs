using System;
using System.Text;

namespace Amv.NPuzzle.Core
{
    public class SolutionNode
    {
        public SolutionNode(Board board, int manhattan, int hamming, int step, SolutionNode parent=null)
        {
            Board = board;
            Manhattan = manhattan;
            Hamming = hamming;
            Step = step;
            Parent = parent;
        }

        public Board Board { get; }

        public SolutionNode Parent { get; }

        /// <summary>
        /// Number of blocks out of place
        /// </summary>
        public int Hamming { get; }

        /// <summary>
        /// Sum of Manhattan distances between blocks and goal
        /// </summary>
        public int Manhattan { get; }

        /// <summary>
        /// Is this board the goal board?
        /// </summary>
        public bool IsGoal => Manhattan == 0;

        public int Step { get; }
    }
}
