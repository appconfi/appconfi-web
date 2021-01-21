using App.SharedKernel.Domain;
using System;

namespace App.Domain
{
    public abstract class TargetRule : Entity<int>
    {
        public abstract string Name { get; }

        public abstract bool IsTarget(TargetUser user);

        public UserTargeting UserTargeting { get; set; }

        public Guid UserTargetingId { get; set; }

    }
}
