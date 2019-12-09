using System;
using System.Linq;

namespace TriangleMaxSumPath
{
    public class TriangleMaxSumPathCalculator
    {
        // Solution explained in README.MD
        public TriangleMaxSumPathResult Calculate(int[] input)
        {
            var inputTriangle = new Triangle<int>(input);
            var resultTriangle = CreateResultsTriangle(input.Length);

            // we initialize bottom values with obvious results
            var row = inputTriangle.Height - 1;
            for (int column = 0; column <= row; column++)
            {
                var currentValue = inputTriangle[row, column];
                resultTriangle[row, column] = new TriangleMaxSumPathResult(currentValue);
            }

            // we fill parents with results
            for (row--; row >= 0; row--)
            {
                for (int column = 0; column <= row; column++)
                {
                    var leftChildResult = resultTriangle[row + 1, column];
                    var rightChildResult = resultTriangle[row + 1, column + 1];

                    var currentValue = inputTriangle[row, column];
                    var leftChildValue = inputTriangle[row + 1, column];
                    var rightChildValue = inputTriangle[row + 1, column + 1];

                    TriangleMaxSumPathResult bestResult = new TriangleMaxSumPathResult();

                    if (IsBetterValidResult(bestResult, currentValue, leftChildResult, leftChildValue))
                    {
                        bestResult = leftChildResult.AppendValue(currentValue);
                    }

                    if (IsBetterValidResult(bestResult, currentValue, rightChildResult, rightChildValue))
                    {
                        bestResult = rightChildResult.AppendValue(currentValue);
                    }

                    resultTriangle[row, column] = bestResult;
                }
            }

            // we return top result
            return resultTriangle[0, 0];
        }

        private bool HaveDifferentParity(int first, int second)
            => (first % 2) != (second % 2);

        private bool IsBetterValidResult(TriangleMaxSumPathResult currentResult, int currentValue,
            TriangleMaxSumPathResult proposedResult, int proposedValue)
        {
            if (proposedResult.MaxSum == null)
                return false;

            if (!HaveDifferentParity(currentValue, proposedValue))
                return false;

            if (currentResult.MaxSum == null)
                return true;

            if (currentResult.MaxSum < proposedResult.MaxSum + currentValue)
                return true;

            return false;
        }

        private Triangle<TriangleMaxSumPathResult> CreateResultsTriangle(int length)
        {
            var array = new TriangleMaxSumPathResult[length];

            return new Triangle<TriangleMaxSumPathResult>(array);
        }
    }
}
