using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit;
using NUnit.Framework;

namespace Amv.NPuzzle.Core.Test
{
    [TestFixture]
    public class PuzzleSolverTest
    {
        [Test]
        [TestCaseSource(typeof(BoardData), nameof(BoardData.TestBoards))]
        public void SolveGivenBoard(BoardTestDataFile file)
        {
            var data = file.Data;
            var solver = new PuzzleSolver(data.Source, data.Target);

            Assert.AreEqual(data.IsSolvable,solver.IsSolvable);

            var result = solver.Solve();

            result.Should().NotBeNull();
            if (data.IsSolvable)
            {
                data.Moves.Should().Be(data.Moves);
                result.IsSolvable.Should().BeTrue();
                result.Solution.First().Should().Be(data.Source);
                result.Solution.Last().Should().Be(data.Target);
                result.Solution.Count.Should().Be(data.Moves+1);
            }
            else
            {
                result.Moves.Should().Be(0);
                result.IsSolvable.Should().BeFalse();
            }
        }
    }
}
