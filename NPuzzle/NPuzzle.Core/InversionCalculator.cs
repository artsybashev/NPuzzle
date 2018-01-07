using System;
using System.Collections.Generic;
using System.Linq;

namespace Amv.NPuzzle.Core
{
    public class InversionCalculator : IHeurisiticCalculator
    {
        public int Calculate(short[,] board)
        {
            int n = board.GetLength(0);
            int result = 0;

            var flatten = new short[n*n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    flatten[i * n + j] = board[i, j];
                }
            }

            for (int i = 0; i < flatten.Length - 1; i++)
            {
                for (int j = i + 1; j < flatten.Length; j++)
                {
                    if (flatten[j]>0 && flatten[i]>0 && flatten[i] > flatten[j])
                        result++;
                }
            }
            return result;
        }
    }
}