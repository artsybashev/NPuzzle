namespace Amv.NPuzzle.Core
{
    public interface IHeurisiticCalculator
    {
        int Calculate(short[,] board);
    }
}