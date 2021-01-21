using App.SharedKernel.Exceptions;
using System;

namespace App.SharedKernel.Guards
{
    public partial class Guard
    {
        static void CheckOrFail(bool condition, Exception e)
        {
            if (!condition)
                throw e;
        }

        static void CheckOrFail(bool condition, string message)
        {
            if (!condition)
                throw new GuardValidationException(message);
        }

        public static void IsNull(object input, string message = "is not null")
        {
            CheckOrFail(input == null, message);
        }

        public static void IsNotNull(object input, string message = "is null")
        {
            CheckOrFail(input != null, message);
        }

        public static void HasValue<T>(T? input, string message = "the input has not value") where T : struct
        {
            CheckOrFail(input.HasValue, message);
        }

        public static void HasNotValue<T>(T? input, string message = "the input has value") where T : struct
        {
            CheckOrFail(!input.HasValue, message);
        }
    }

    public class GuardValidationException : AppException
    {
        public GuardValidationException(string message) : base(message)
        {

        }
    }
}
