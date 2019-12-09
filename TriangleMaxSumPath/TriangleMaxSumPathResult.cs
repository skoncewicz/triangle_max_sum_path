using System;
using System.Collections.Generic;
using System.Linq;

namespace TriangleMaxSumPath
{
    public class TriangleMaxSumPathResult
    {
        public int? MaxSum { get; private set; }
        public IReadOnlyList<int> Path { get; private set; }

        public TriangleMaxSumPathResult()
        {
            MaxSum = null;
            Path = new int[] { };
        }

        public TriangleMaxSumPathResult(int value)
        {
            MaxSum = value;
            Path = new int[] { value };
        }

        public TriangleMaxSumPathResult AppendValue(int value)
        {
            if (MaxSum == null)
                throw new InvalidOperationException("Cannot append value to invalid result.");

            return new TriangleMaxSumPathResult
            {
                MaxSum = MaxSum + value,
                Path = new int[] { value }.Concat(Path).ToArray()
            };
        }

        public TriangleMaxSumPathResult(int maxSum, params int[] path)
        {
            if (path.Sum() != maxSum)
                throw new ArgumentException("Incorrect result. Path does not sum up to value.");

            MaxSum = maxSum;
            Path = path;
        }
    }
}
