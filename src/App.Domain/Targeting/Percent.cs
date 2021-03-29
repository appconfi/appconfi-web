using App.SharedKernel.Domain;
using App.SharedKernel.Guards;

namespace App.Domain
{
    public class Percent : ValueObject
    {
        public int Number { get; set; }

        public static Percent FromNumber(int number)
        {
            Guard.GreaterOrEqualThan(number, 0, "Invalid percentage");
            Guard.IsTrue(number <= 100, "Invalid percentage");

            return new Percent
            {
                Number = number
            };
        }

        public void SetNumber(int number)
        {
            Guard.GreaterOrEqualThan(number, 0, "Invalid percentage");
            Guard.IsTrue(number <= 100, "Invalid percentage");
            Number = number;
        }
    }
}
