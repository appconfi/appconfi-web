using App.SharedKernel.Domain;
using App.SharedKernel.Guards;
using App.SharedKernel.Specifications;
using System;

namespace App.Domain
{
    public class ActivityLog : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime TimeStamp { get; set; }

        public virtual Application Application { get; set; }

        public Guid ApplicationId { get; set; }

        public virtual User InitiatedBy { get; set; }

        public Guid InitiatedById { get; set; }

        public ActivityLogStatus Status { get; set; }

        public static ActivityLog SuccessLog(string name, string description, Guid applicationId, Guid userId)
        {
            Guard.IsNotNullOrEmpty(name);

            return new ActivityLog
            {
                Name = name,
                Description = description,
                ApplicationId = applicationId,
                InitiatedById = userId,
                TimeStamp = DateTime.UtcNow,
                Status = ActivityLogStatus.Success,
                Id = Guid.NewGuid(),
            };
        }

        public static ISpecification<ActivityLog> ByApplication(Guid applicationId)
        {
            return new DirectSpecification<ActivityLog>(x => x.ApplicationId == applicationId);
        }
    }
}
