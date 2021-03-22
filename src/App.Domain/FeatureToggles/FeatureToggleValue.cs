using App.SharedKernel.Domain;
using App.SharedKernel.Specifications;
using System;
using System.Linq;

namespace App.Domain.FeatureToggles
{
    public class FeatureToggleValue : Entity
    {
        public const string FEATURE_STRING_ON = "on";
        public const string FEATURE_STRING_OFF = "off";

        public static FeatureToggleValue Parse(string value)
        {
            var supportedStrings = new string[] { "on", "true", "1", "yes", "ok" };
            var isEnabled = supportedStrings.Contains(value.ToLower());
            return new FeatureToggleValue { IsEnabled = isEnabled };

        }


        public bool IsEnabled { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? LastReadAt { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        public ApplicationEnvironment Environment { get; set; }

        public FeatureToggle FeatureToggle { get; set; }

        public Guid EnvironmentId { get; set; }

        public Guid FeatureToggleId { get; set; }

        public static FeatureToggleValue NewValue(bool value, Guid environmentId, Guid featureToggleId)
        {
            return new FeatureToggleValue
            {
                Id = Guid.NewGuid(),
                EnvironmentId = environmentId,
                FeatureToggleId = featureToggleId,
                CreatedAt = DateTime.UtcNow,
                LastModifiedAt = DateTime.UtcNow,
                IsEnabled = value
            };
        }

        private void UpdateValue(bool enabled)
        {
            IsEnabled = enabled;
            LastModifiedAt = DateTime.UtcNow;
        }

        public void Enable()
        {
            UpdateValue(true);
        }

        public void Disable()
        {
            UpdateValue(false);
        }

        public static ISpecification<FeatureToggleValue> WithEnvironment(Guid envId)
        {
            return new DirectSpecification<FeatureToggleValue>(x => x.EnvironmentId == envId);
        }

        public override string ToString()
        {
            return IsEnabled ? FEATURE_STRING_ON : FEATURE_STRING_OFF;
        }
    }
}
