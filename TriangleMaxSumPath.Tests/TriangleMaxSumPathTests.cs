using Xunit;
using FluentAssertions;

namespace TriangleMaxSumPath.Tests
{
    public class TriangleMaxSumPathTests
    {
        private readonly TriangleMaxSumPathCalculator _sut = new TriangleMaxSumPathCalculator();

        [Fact]
        public void ShouldCalculateCorrectResultForSampleCase0()
        {
            var input = new int[]
            {
                1,
                2, 4,
                5, 7, 9
            };

            var result = _sut.Calculate(input);

            var expectedResult = new TriangleMaxSumPathResult
            (
                maxSum: 14,
                path: new[] { 1, 4, 9 }
            );

            AssertResultMatchesExpected(result, expectedResult);
        }

        [Fact]
        public void ShouldCalculateCorrectResultForSampleCase1()
        {
            var input = new int[]
            {
                1,
                8, 9,
                1, 5, 9,
                4, 5, 2, 3
            };

            var result = _sut.Calculate(input);

            var expectedResult = new TriangleMaxSumPathResult
            (
                maxSum: 16,
                path: new[] { 1, 8, 5, 2 }
            );

            AssertResultMatchesExpected(result, expectedResult);
        }

        [Fact]
        public void ShouldCalculateCorrectResultForSampleCase3()
        {
            var input = new int[]
            {
                1,
                4, 2,
            };

            var result = _sut.Calculate(input);

            var expectedResult = new TriangleMaxSumPathResult
            (
                maxSum: 5,
                path: new[] { 1, 4 }
            );

            AssertResultMatchesExpected(result, expectedResult);
        }

        [Fact]
        public void ShouldCalculateCorrectResultForSampleCase4()
        {
            var input = new int[]
            {
                1,
                2, 4,
            };

            var result = _sut.Calculate(input);

            var expectedResult = new TriangleMaxSumPathResult
            (
                maxSum: 5,
                path: new[] { 1, 4 }
            );

            AssertResultMatchesExpected(result, expectedResult);
        }

        [Fact]
        public void ShouldReturnEmptyResultWhenNoValidPathPresent()
        {
            var input = new int[]
            {
                1,
                1, 1,
                1, 1, 1,
                1, 1, 1, 1
            };

            var result = _sut.Calculate(input);

            var expectedResult = new TriangleMaxSumPathResult();

            AssertResultMatchesExpected(result, expectedResult);
        }

        private void AssertResultMatchesExpected(TriangleMaxSumPathResult result,
            TriangleMaxSumPathResult expectedResult)
        {
            result.MaxSum.Should().Be(expectedResult.MaxSum);
            result.Path.Should().BeEquivalentTo(expectedResult.Path, opt => opt.WithStrictOrdering());
        }
    }
}
