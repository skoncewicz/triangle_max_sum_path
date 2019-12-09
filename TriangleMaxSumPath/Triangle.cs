using System;

namespace TriangleMaxSumPath
{
    internal class Triangle<TItem>
    {
        public int Height { get; private set; }

        public TItem this[int h, int i]
        {
            get => input[CalculateIndex(h, i)];
            set => input[CalculateIndex(h, i)] = value;
        }

        public Triangle(TItem[] input)
        {
            var length = input.Length;

            if (length == 0 || !IsTriangleLength(length))
                throw new ArgumentException("Provided array cannot be arranged into triangle.", nameof(input));

            this.input = input;
            Height = TriangleHeight(length);
        }

        private readonly TItem[] input;

        private bool IsTriangleLength(int length)
        {
            // number of numbers (n) in a triangle of height = h is equal to n = 1 + 2 + ... + h
            // therefore total number of items in such triangle triangle is 
            // n = h(h+1)/2 (from: https://en.wikipedia.org/wiki/1_%2B_2_%2B_3_%2B_4_%2B_%E2%8B%AF)
            // natural h such that h(h+1)/2 = length must exist
            // 0.5h^2 + 0.5h = length
            // 0.5h^2 + 0.5h - length = 0 / * 2
            // h^2 + h - 2 * length = 0
            // therefore from https://en.wikipedia.org/wiki/Quadratic_formula
            // delta = 1 - 4 * (-2) * length = 1 + 8*length
            // h = ( -1 +/- sqrt(1 + 8*length) ) / 2
            // we can discard negative solution
            // h = (sqrt(1 + 8 * length))/2 must be integer
            // and that will occur if and only if 1 + 8 * length can be expressed as k^2 where k is integer

            if (length < 0)
                return false;

            return IsPerfectSquare(1 + 8 * length);
        }

        private static bool IsPerfectSquare(int value)
        {
            var test = (int)(Math.Sqrt(value) + 0.5);

            return test * test == value;
        }

        // this equation is explained in comment for IsTriangleLength(int length) function
        private int TriangleHeight(int length)
            => (int)(-1 + Math.Sqrt(1 + 8 * length)) / 2;

        private int TriangleLength(int height)
            => height * (height + 1) / 2;

        private int CalculateIndex(int row, int column)
        {
            if (row >= Height || column > row)
                throw new IndexOutOfRangeException();

            return TriangleLength(row) + column;
        }
    }
}
