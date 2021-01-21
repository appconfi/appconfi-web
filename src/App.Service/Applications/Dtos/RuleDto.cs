using App.Domain;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace App.Service.Applications
{
    public class RuleDto
    {
        public class EnabledForDto
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("parameters")]
            public IDictionary<string, object> Parameters { get; set; }
        }

        [JsonProperty("isEnabled")]
        public bool IsEnabled { get; set; }

        [JsonProperty("enabledFor")]
        public EnabledForDto EnabledFor { get; set; }

        public static RuleDto NewRule(bool isEnabled, TargetRule rule)
        {
            if (rule is TargetPercentage)
                return New(isEnabled, rule as TargetPercentage);
            if (rule is TargetSpecificUsers)
                return New(isEnabled, rule as TargetSpecificUsers);

            return null;
        }
        private static RuleDto New(bool isEnabled, TargetPercentage rule)
        {
            return new RuleDto
            {
                IsEnabled = isEnabled,
                EnabledFor = new EnabledForDto
                {
                    Name = rule.Name,
                    Parameters = new Dictionary<string, object> {
                        { "percent", rule.Percent.Number }
                    },
                }
            };
        }
        private static RuleDto New(bool isEnabled, TargetSpecificUsers rule)
        {
            return new RuleDto
            {
                IsEnabled = isEnabled,
                EnabledFor = new EnabledForDto
                {
                    Name = rule.Name,
                    Parameters = new Dictionary<string, object> {
                        { "option", rule.Option.ToString().ToLower() },
                        { "property", rule.Property },
                        { "values", rule?.ValuesList?.List }
                    }
                }
            };
        }
    }
}
