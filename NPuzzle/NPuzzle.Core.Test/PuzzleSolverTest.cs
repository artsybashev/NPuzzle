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
                {5, 1, 3, 2},
                {10, 6, 15, 7},
                {9, 8, 11, 4},
                {0, 13, 14, 12}
            });
            var solver = new PuzzleSolver(source, target);
            var result = solver.Solve();
            
            Assert.NotNull(result);
        }

        [Theory]
        [MemberData(nameof(BoardData.TestBoards), MemberType = typeof(BoardData))]
        public void SolveGivenBoard(BoardTestDataFile file)
        {
            var data = file.Data;
            var solver = new PuzzleSolver(data.Source, data.Target);
            var result = solver.Solve();

            Assert.Equal(data.IsSolvable, solver.IsSolvable);
            Assert.NotNull(result);
            Assert.Equal(data.Moves, result.Moves);
        }
    }
}
