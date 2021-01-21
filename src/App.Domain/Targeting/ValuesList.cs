using App.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Domain
{
    public class ValuesList : ValueObject
    {
        public string List { get; set; }

        public static ValuesList NewList(string list)
        {
            var formattedList = list.Split(',').Select(x => x.Trim());
            string joined = string.Join(",", formattedList);
            return new ValuesList
            {
                List = joined
            };
        }

        public override string ToString()
        {
            return List;
        }

        public IEnumerable<string> Values => List.Split(',');

        public bool Apply(string value, TargetOption option)
        {
            if (option == TargetOption.Contains)
                return Values.Any(x => x.Contains(value));

            if (option == TargetOption.NotContains)
                return Values.Any(x => !x.Contains(value));

            if (option == TargetOption.IsIn)
                return Values.Contains(value);

            if (option == TargetOption.IsNotIn)
                return !Values.Contains(value);

            return false;
        }
    }
}
