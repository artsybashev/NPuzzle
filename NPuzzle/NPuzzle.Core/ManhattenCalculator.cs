using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Amv.NPuzzle.Core
{
    public class ManhattenCalculator : IManhattenCalculator
    {
        public int GetManhattan(short[,] board, (short Row, short Col)[] targetBoardMap)
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
                    var targetPos = targetBoardMap[val];
                    result += Math.Abs(i - targetPos.Row) + Math.Abs(j - targetPos.Col);
                }
            }
            return result;
        }

        private (short, short)[] GetTargetBoard(int dimmention)
        {
            var result = new ValueTuple<short, short>[dimmention * dimmention];
            for (short i = 0; i < dimmention; i++)
            {
                for (short j = 0; j < dimmention; j++)
                {
                    result[i * dimmention + j] = (i, j);
                }
            }
            return result;
        }
    }


}
