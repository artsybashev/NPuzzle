using System.Collections.Generic;

namespace Amv.NPuzzle.Core
{
    public class PuzzleResult
    {
        public PuzzleResult(IReadOnlyCollection<Board> solution)
        {
            Solution = solution;
        }

        public IReadOnlyCollection<Board> Solution { get; }
        public int Moves => Solution.Count - 1;
    }
}