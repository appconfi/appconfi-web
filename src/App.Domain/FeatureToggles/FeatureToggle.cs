using App.SharedKernel.Domain;
using App.SharedKernel.Guards;
using App.SharedKernel.Specifications;
using System;

namespace App.Domain.FeatureToggles
{
    public class FeatureToggle : Entity, ICreatedAt
    {
        public string Key { get; set; }

        public DateTime CreatedAt { get; set; }

        public Application Application { get; set; }

        public Guid ApplicationId { get; set; }

        public string Description { get; set; }

        public static ISpecification<FeatureToggle> WithApplication(Guid appId)
        {
            return new DirectSpecification<FeatureToggle>(x => x.ApplicationId == appId);
        }
        public static FeatureToggle New(string key, string description, Application application)
        {
            Guard.IsNotNullOrEmpty(key, "Invalid key for feature toggle");
            Guard.IsNotNull(application, "Invalid application");
            Guard.HasMaxLength(description ?? "", 2000, "Description length is greater than the maximum required");

            return new FeatureToggle
            {
                Application = application,
                ApplicationId = application.Id,
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Key = key,
                Description = description
            };
        }
    }
}
