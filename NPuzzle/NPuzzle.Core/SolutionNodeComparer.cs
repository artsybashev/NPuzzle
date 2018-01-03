using System.Collections.Generic;

namespace Amv.NPuzzle.Core
{
    public class SolutionNodeComparer : IComparer<SolutionNode>
    {
        public int Compare(SolutionNode x, SolutionNode y)
        {
            if (x == null)
            {
                return y == null ? 0 : -1;
            }
            else if (y == null)
            {
                return 1;
            }

            int result = x.Manhattan + x.Step - y.Manhattan - y.Step;
            if (result == 0)
            {
                result = x.Hamming + x.Step - y.Hamming - y.Step;
            }
            return result;
        }
    }
}