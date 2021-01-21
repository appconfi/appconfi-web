using App.SharedKernel;

namespace App.Domain
{
    public class TargetPercentage : TargetRule
    {
        public Percent Percent { get; set; }

        public override string Name => "percentage";

        public override bool IsTarget(TargetUser user)
        {
            if (Percent.Number == 0)
                return false;

            var hashId = Security.CreateMD5(user["id"] + UserTargeting.EnvironmentId.ToString()).ToLower();
            int value = int.Parse(hashId[0].ToString(), System.Globalization.NumberStyles.HexNumber);


            var number = (Percent.Number / 100.0) * 16;
            return value <= number;
        }

        public static TargetPercentage New(int percent, UserTargeting userTargetting)
        {
            return new TargetPercentage
            {
                Percent = Percent.FromNumber(percent),
                UserTargeting = userTargetting,
                UserTargetingId = userTargetting.Id
            };
        }

        public override string ToString()
        {
            return $"For {Percent.Number}% of users";
        }
    }
}
