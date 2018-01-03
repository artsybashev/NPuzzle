using System;

namespace Amv.NPuzzle.Core
{
    public class HammingCalculator : IHammingCalculator
    {
        public int Calculate(short[,] board, (short Row, short Col)[] targetBoardMap)
        {
            int n = board.GetLength(0);
            int result = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    var val = board[i, j];
                    if (val == 0)
                        continue;
                    var targetPos = targetBoardMap[val];
                    result += i == targetPos.Row && j == targetPos.Col ? 0 : 1;
                }
            }
            return result;
        }
    }
}