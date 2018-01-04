namespace Amv.NPuzzle.Core.Test
{
    public class BoardTestData
    {
        public BoardTestData(int moves, Board source, Board target, bool isSolvable)
        {
            Moves = moves;
            Source = source;
            Target = target;
            IsSolvable = isSolvable;
        }

        public int Moves { get; }
        public Board Source { get; }
        public Board Target { get; }
        public bool IsSolvable { get; }
    }
}