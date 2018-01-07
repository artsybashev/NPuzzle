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

            int result = x.Priority0 + x.Step - y.Priority0 - y.Step;
            if (result == 0)
            {
                result = x.Priority1 + x.Step - y.Priority1 - y.Step;
            }
            /*int result = x.Priority0 + x.Priority1 - y.Priority0 - y.Priority1;
            if (result == 0)
            {
                result = x.Step - y.Step;
            }*/
            return result;
}
}
}
