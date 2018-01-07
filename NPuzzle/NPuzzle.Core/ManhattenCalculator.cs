using System;

namespace Amv.NPuzzle.Core
{
    public class ManhattenCalculator : IHeurisiticCalculator
    {
        private readonly (short Row, short Col)[] _targetBoardMap;

        public ManhattenCalculator((short Row, short Col)[] targetBoardMap)
        {
            _targetBoardMap = targetBoardMap;
        }

        public int Calculate(short[,] board)
        {
            int n = board.GetLength(0);
            int result = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    var val = board[i, j];
                    if(val==0)
                        continue;
                    var targetPos = _targetBoardMap[val];
                    result += Math.Abs(i - targetPos.Row) + Math.Abs(j - targetPos.Col);
                }
            }
            return result;
        }
    }
}
