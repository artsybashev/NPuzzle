using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Amv.NPuzzle.Core.Test
{
    public class PuzzleSolverTest
    {
        [Fact]
        public void Test()
        {
            var target = Board.GetClassicalGoal(4);
            var source = new Board(new short[,]
            {
                {6, 2, 5, 3},
                {9, 7, 1, 14},
                {10, 0, 13, 8},
                {11, 12, 4, 15}
            });
            var solver = new PuzzleSolver(source, target);
            var result = solver.Solve();
            
            Assert.NotNull(result);
        }
    }
}
