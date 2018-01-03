namespace Amv.NPuzzle.Core
{
    public interface IHammingCalculator
    {
        int Calculate(short[,] board, (short Row, short Col)[] targetBoardMap);
    }
}