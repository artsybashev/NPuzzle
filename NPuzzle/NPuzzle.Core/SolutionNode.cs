using System;
using System.Text;

namespace Amv.NPuzzle.Core
{
    public class SolutionNode
    {
        public SolutionNode(Board board, int manhattan, int hamming, int step, SolutionNode parent=null)
        {
            Board = board;
            Priority0 = manhattan;
            Priority1 = hamming;
            Step = step;
            Parent = parent;
        }

        public Board Board { get; }

        public SolutionNode Parent { get; }

        /// <summary>
        /// Number of blocks out of place
        /// </summary>
        public int Priority1 { get; }

        /// <summary>
        /// Sum of Priority0 distances between blocks and goal
        /// </summary>
        public int Priority0 { get; }

        /// <summary>
        /// Is this board the goal board?
        /// </summary>
        public bool IsGoal => Priority0 == 0;

        public int Step { get; }
    }
}
