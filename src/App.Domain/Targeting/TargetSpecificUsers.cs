using App.SharedKernel.Guards;

namespace App.Domain
{
    public class TargetSpecificUsers : TargetRule
    {

        public ValuesList ValuesList { get; set; }

        public TargetOption Option { get; set; }

        public string Property { get; set; }

        public override string Name => "property";

        public override bool IsTarget(TargetUser user)
        {
            if (!user.ContainsKey(Property))
                return false;

            var value = user[Property];
            return ValuesList.Apply(value, Option);
        }

        public static TargetSpecificUsers New(UserTargeting userTargeting, string property, TargetOption option, string valueList)
        {
            Guard.IsNotNullOrEmpty(property, "Invalid property");
            Guard.IsNotNullOrEmpty(valueList, "Invalid userList");

            return new TargetSpecificUsers
            {
                Option = option,
                Property = property.ToLower(),
                UserTargetingId = userTargeting.Id,
                UserTargeting = userTargeting,
                ValuesList = ValuesList.NewList(valueList)
            };
        }

        public override string ToString()
        {
            switch (Option)
            {
                case TargetOption.IsIn:
                    return $"For users with '{Property}' in the list";
                case TargetOption.IsNotIn:
                    return $"For users with '{Property}' not in the list";
                case TargetOption.Contains:
                    return $"For users with '{Property}' that contains a value in the list";
                case TargetOption.NotContains:
                    return $"For users with '{Property}' that not contains a value";
                default:
                    break;
            }
            return base.ToString();
        }
    }
}
