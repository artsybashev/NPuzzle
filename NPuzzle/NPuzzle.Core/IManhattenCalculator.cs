namespace Amv.NPuzzle.Core
{
    public interface IManhattenCalculator
    {
        int GetManhattan(short[,] board, (short Row, short Col)[] targetBoardMap);
    }
}