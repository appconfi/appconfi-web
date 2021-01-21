using System;
using System.Text.RegularExpressions;

namespace App.SharedKernel.Guards
{
    public partial class Guard
    {
        public static void IsNullOrEmpty(string input, string message = "the text is not null or empty")
        {
            CheckOrFail(String.IsNullOrEmpty(input), message);
        }

        public static void IsNotNullOrEmpty(string input, string message = "the text is null or empty")
        {
            CheckOrFail(!String.IsNullOrEmpty(input), message);
        }

        public static void IsValidEmail(string email, string message = "the email format is incorrect")
        {
            Match(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", message);
        }

        public static void Match(string input, string pattern, string message = "do not match")
        {
            var regex = new Regex(pattern);
            var match = regex.Match(input);
            CheckOrFail(match.Success, message);
        }

        public static void IsEmpty(string input, string message = "the string is not empty")
        {
            HasLength(input, 0, message);
        }

        public static void IsDate(string input, string message = "the string is not a date")
        {
            CheckOrFail(DateTime.TryParse(input, out DateTime result), message);
        }

        public static void HasLength(string input, int length, string message = "the length is incorrect")
        {
            IsNotNull(input, message);
            CheckOrFail(input.Length == length, message);
        }

        public static void HasMaxLength(string input, int length, string message = "the length is greater than the maximum required")
        {
            IsNotNull(input, message);
            CheckOrFail(input.Length <= length, message);
        }

        public static void HasMinLength(string input, int length, string message = "the length is lower than the minimum required")
        {
            IsNotNull(input, message);
            CheckOrFail(input.Length >= length, message);
        }
    }
}
