namespace Amv.NPuzzle.Core
{
    public class LinearConflictCalculator : IHeurisiticCalculator
    {
        private readonly (short Row, short Col)[] _targetBoardMap;

        public LinearConflictCalculator((short Row, short Col)[] targetBoardMap)
        {
            _targetBoardMap = targetBoardMap;
        }

        public int Calculate(short[,] board)
        {
            int n = board.GetLength(0);
            int linearConflict = 0;

            for (int i = 0; i < n; i++)
            {
                short maxH = -1;
                short maxV = -1;
                for (int j = 0; j < n; j++)
                {
                    var valH = board[i, j];
                    var targetH = _targetBoardMap[valH];
                    //is tile in its goal row ?
                    if (valH > 0 && targetH.Row == i)
                    {
                        if (valH > maxH)
                        {
                            maxH = valH;
                        }
                        else
                        {
                            linearConflict += 2;
                        }
                    }
                    var valV = board[j, i];
                    var targetV = _targetBoardMap[valV];
                    if (valV > 0 && targetV.Col == i)
                    {
                        if (valV > maxV)
                        {
                            maxV = valV;
                        }
                        else
                        {
                            linearConflict += 2;
                        }
                    }
                }

            }
            return linearConflict;
        }

    }
}