using FluentAssertions;
using System;
using Xunit;

namespace TriangleMaxSumPath.Tests
{
    public class TriangleTests
    {
        [Fact]
        public void ShouldNotThrowWhenInputIsSingleNumber()
        {
            var input = new int[] { 1 };

            AssertInputIsValid(input);
            AssertHeightIs(input, 1);
        }

        [Fact]
        public void ShouldNotThrowForValidTriangle()
        {
            var input = new int[]
            {
                1,
                8, 9,
                1, 5, 9,
                4, 5, 2, 3
            };

            AssertInputIsValid(input);
            AssertHeightIs(input, 4);
        }

        [Fact]
        public void ShouldThrowWhenInputCannotBeArrangedIntoTriangle()
        {
            var input = new int[]
            {
                1,
                2, 3,
                4, 5
            };

            AssertInputIsInvalid(input);
        }

        [Fact]
        public void ShouldThrowWhenInputIsEmpty()
        {
            AssertInputIsInvalid(new int[]{ });
        }

        [Fact]
        public void ShouldAllowIndexingGetters()
        {
            var input = new int[]
            {
                1,
                8, 9,
                1, 5, 9,
                4, 5, 2, 3
            };

            var triangle = new Triangle<int>(input);

            triangle[0, 0].Should().Be(1);
            triangle[2, 1].Should().Be(5);
            triangle[2, 2].Should().Be(9);
            triangle[3, 3].Should().Be(3);
        }


        [Fact]
        public void ShouldAllowIndexingSetters()
        {
            var input = new int[]
            {
                1,
                8, 9,
                1, 5, 9,
                4, 5, 2, 3
            };

            var triangle = new Triangle<int>(input);
            triangle[2, 1] = -2;

            triangle[2, 1].Should().Be(-2);
        }

        [Fact]
        public void ShouldThrowOnIncorrectIndex()
        {
            var input = new int[]
            {
                1,
                8, 9,
                1, 5, 9,
                4, 5, 2, 3
            };

            var triangle = new Triangle<int>(input);

            Action illegalCollumnAccess =
                () => { var _ = triangle[1, 2]; };

            Action illegalRowAccess =
                () => { var _ = triangle[4, 2]; };

            illegalCollumnAccess.Should().Throw<IndexOutOfRangeException>();
            illegalRowAccess.Should().Throw<IndexOutOfRangeException>();
        }

        private void AssertInputIsInvalid(int[] input)
        {
            Action invalidConstruction =
                () => new Triangle<int>(input);

            invalidConstruction.Should().Throw<ArgumentException>();
        }

        private void AssertInputIsValid(int[] input)
        {
            var _ = new Triangle<int>(input);
        }

        private void AssertHeightIs(int[] input, int expectedHeight)
        {
            var triangle = new Triangle<int>(input);

            triangle.Height.Should().Be(expectedHeight);
        }
    }
}
