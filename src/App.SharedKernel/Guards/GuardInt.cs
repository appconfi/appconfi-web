using System;

namespace App.SharedKernel.Guards
{
    public partial class Guard
    {
        public static void GreaterThan<T>(T input, T value, string message = "input is not greater than value") where T : IComparable
        {
            CheckOrFail(input.CompareTo(value) == 1, message);
        }

        public static void GreaterOrEqualThan<T>(T input, T value, string message = "input is not greater or equal than value") where T : IComparable
        {
            CheckOrFail(input.CompareTo(value) >= 0, message);
        }
    }
}
