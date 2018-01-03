using System;
using System.Collections.Generic;
using System.Text;

namespace Amv.NPuzzle.Core.Test
{
    public static class BoardData
    {
        public static int[][] Solved3X3 = new int[][] { new int[] { 1,2,3}, new int[] { 4,5,6}, new int[] { 7,8,0}};
        public static int[][] Solved4X4 = new int[][] { new int[] { 1,2,3,4}, new int[] { 5,6,7,8}, new int[] { 9,10,11,12}, new int[] { 13, 14, 15, 0 } };
        public static int[][] UnsolvedInverted3X3 = new int[][] { new int[] { 0,8,7}, new int[] { 6,5,4}, new int[] { 3,2,1}};
    }
}
