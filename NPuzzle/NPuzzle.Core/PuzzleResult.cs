using System.Collections.Generic;

namespace Amv.NPuzzle.Core
{
    public class PuzzleResult
    {
        public static readonly PuzzleResult Unsolvable = new PuzzleResult();

        private PuzzleResult()
        {
        }

        public PuzzleResult(IReadOnlyCollection<Board> solution)
        {
            Solution = solution;
            Moves = Solution.Count - 1;
            IsSolvable = true;
        }

        public IReadOnlyCollection<Board> Solution { get; }
        public int Moves { get; }
        public bool IsSolvable { get; }
    }
}