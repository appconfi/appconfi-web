using System;

namespace App.SharedKernel.Guards
{
    public partial class Guard
    {
        public static void IsTrue(bool input, Exception e)
        {
            CheckOrFail(input, e);
        }

        public static void IsTrue(bool input, string message = "the value is false")
        {
            CheckOrFail(input, message);
        }

        public static void IsFalse(bool input, string message = "the value is true")
        {
            CheckOrFail(!input, message);
        }
    }
}
