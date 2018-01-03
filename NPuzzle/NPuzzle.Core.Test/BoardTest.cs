using System;
using Amv.NPuzzle.Core;
using Xunit;

namespace Amv.NPuzzle.Core.Test
{
    public class BoardTest
    {
         

        [Fact]
        public void GetHashCode_EqualForSameBoardsButDifferentObjects()
        {
            var board1 = new Board(BoardData.Solved3X3);
            var board2 = new Board(BoardData.Solved3X3);
            Assert.Equal(board1.GetHashCode(), board2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_EqualForDifferentBoards()
        {
            var board1 = new Board(BoardData.Solved3X3);
            var board2 = new Board(BoardData.UnsolvedInverted3X3);
            Assert.NotEqual(board1.GetHashCode(), board2.GetHashCode());
        }

        [Fact]
        public void Equals_TrueForSameBoardsButDifferentObjects()
        {
            var board1 = new Board(BoardData.Solved3X3);
            var board2 = new Board(BoardData.Solved3X3);
            Assert.True(board1.Equals(board2));
        }

        [Fact]
        public void Equals_FalseForDifferentBoards()
        {
            var board1 = new Board(BoardData.Solved3X3);
            var board2 = new Board(BoardData.UnsolvedInverted3X3);
            Assert.False(board1.Equals(board2));
        }

        [Fact]
        public void ToString_ReturnsElementsFormatedAsSquare()
        {
            var expected = "1 2 3\r\n4 5 6\r\n7 8 0";

            var board = new Board(BoardData.Solved3X3);
            
            Assert.Equal(expected, board.ToString());
        }

        [Fact]
        public void ToString_AddsPadding()
        {
            var expected = " 1  2  3  4\r\n 5  6  7  8\r\n 9 10 11 12\r\n13 14 15  0";

            var board = new Board(BoardData.Solved4X4);

            Assert.Equal(expected, board.ToString());
        }
    }
}
