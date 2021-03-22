using App.Domain.FeatureToggles;
using App.SharedKernel.Domain;
using App.SharedKernel.Guards;
using App.SharedKernel.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Domain
{
    public class ApplicationEnvironment : Entity
    {
        public virtual Application Application { get; set; }

        public Guid ApplicationId { get; set; }

        public string Name { get; set; }

        public bool IsDefault { get; set; }

        public DateTime? LastUpdateAt { get; set; }

        public ICollection<FeatureToggleValue> FeatureToggleValues { get; set; }

        public static ApplicationEnvironment NewEnv(Guid applicationId, string name, bool isDefault = false)
        {
            Guard.IsNotNullOrEmpty(name, "Undefined name for the environment");
            Guard.IsNotNull(applicationId, "Invalid application");

            return new ApplicationEnvironment
            {
                ApplicationId = applicationId,
                Id = Guid.NewGuid(),
                Name = name,
                IsDefault = isDefault,
                LastUpdateAt = DateTime.UtcNow,
                FeatureToggleValues = new List<FeatureToggleValue>()
            };
        }

        public FeatureToggleValue AddOrEditFeatureToggleValue(string key, string description, bool isEnabled)
        {
            var application = Application;
            var toggle = application.FeatureToggles.FirstOrDefault(x => x.Key == key);

            if (toggle == null)
                toggle = application.AddNewFeatureToggle(key, description);

            var featureValue = FeatureToggleValues.FirstOrDefault(x => x.FeatureToggleId == toggle.Id);
            if (featureValue == null)
            {
                featureValue = FeatureToggleValue.NewValue(isEnabled, Id, toggle.Id);
                FeatureToggleValues.Add(featureValue);
            }
            else
            {
                if (isEnabled)
                    featureValue.Enable();
                else
                    featureValue.Disable();

            }
            LastUpdateAt = DateTime.UtcNow;

            return featureValue;
        }

        public FeatureToggleValue AddOrEditFeatureToggleValue(Guid featureToggleId, bool isEnabled)
        {
            var featureToggleValue = FeatureToggleValues.FirstOrDefault(x => x.FeatureToggleId == featureToggleId);
            if (featureToggleValue == null)
            {
                featureToggleValue = FeatureToggleValue.NewValue(isEnabled, Id, featureToggleId);
                FeatureToggleValues.Add(featureToggleValue);
            }
            else
            {
                if (isEnabled)
                    featureToggleValue.Enable();
                else
                    featureToggleValue.Disable();
            }

            LastUpdateAt = DateTime.UtcNow;

            return featureToggleValue;
        }

        public long GetVersion()
        {
            return LastUpdateAt.GetValueOrDefault(new DateTime()).Ticks;
        }

        #region Specifications

        public static ISpecification<ApplicationEnvironment> WithApplication(Guid applicationId)
        {
            return new DirectSpecification<ApplicationEnvironment>(x => x.ApplicationId == applicationId);
        }
        public static ISpecification<ApplicationEnvironment> WithName(string name)
        {
            return new DirectSpecification<ApplicationEnvironment>(x => x.Name == name);
        }

        #endregion
    }
}
