using App.Domain.FeatureToggles;
using App.SharedKernel.Domain;
using App.SharedKernel.Specifications;
using System;

namespace App.Domain
{
    public class UserTargeting : Entity
    {
        public FeatureToggle FeatureToggle { get; set; }

        public ApplicationEnvironment Environment { get; set; }

        public Guid EnvironmentId { get; set; }

        public Guid FeatureToggleId { get; set; }

        public TargetRule TargetRule { get; set; }

        public int TargetId { get; set; }

        public static ISpecification<UserTargeting> ByEnvironment(Guid env)
        {
            return new DirectSpecification<UserTargeting>(x => x.EnvironmentId == env);
        }

        public static ISpecification<UserTargeting> ById(Guid id)
        {
            return new DirectSpecification<UserTargeting>(x => x.Id == id);
        }

        public static ISpecification<UserTargeting> ByApplication(Guid app)
        {
            return new DirectSpecification<UserTargeting>(x => x.Environment.ApplicationId == app);
        }

        public static ISpecification<UserTargeting> ByToggle(Guid id)
        {
            return new DirectSpecification<UserTargeting>(x => x.FeatureToggleId == id);
        }
        public static ISpecification<UserTargeting> ByToggle(string key)
        {
            return new DirectSpecification<UserTargeting>(x => x.FeatureToggle.Key == key);
        }

        public static UserTargeting PerPercent(Guid environmentId, Guid featureToggleId, int percent)
        {
            var userTargeting = new UserTargeting
            {
                Id = Guid.NewGuid(),
                EnvironmentId = environmentId,
                FeatureToggleId = featureToggleId,
            };

            var target = TargetPercentage.New(percent, userTargeting);
            userTargeting.TargetRule = target;

            return userTargeting;
        }

        public static UserTargeting PerUser(Guid environmentId, Guid featureToggleId, TargetOption option, string property, string users)
        {
            var userTargeting = new UserTargeting
            {
                Id = Guid.NewGuid(),
                EnvironmentId = environmentId,
                FeatureToggleId = featureToggleId,
            };

            var target = TargetSpecificUsers.New(userTargeting, property, option, users);
            userTargeting.TargetRule = target;

            return userTargeting;
        }

    }
}
