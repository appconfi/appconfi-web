using App.SharedKernel.Domain;
using System;

namespace App.Domain
{
    public class ApplicationAccessKey : Entity, ICreatedAt
    {
        public Guid ApplicationId { get; set; }

        public virtual Application Application { get; set; }

        public string Secret { get; set; }

        public DateTime CreatedAt { get; set; }

        public static ApplicationAccessKey NewKey(Guid applicatonId)
        {
            var key = new ApplicationAccessKey
            {
                ApplicationId = applicatonId,
                Id = Guid.NewGuid()
            };

            key.Generate();
            return key;
        }


        public ApplicationAccessKey Generate()
        {
            Secret = $"{Guid.NewGuid().ToString().Replace("-", String.Empty)}";
            CreatedAt = DateTime.UtcNow;

            return this;
        }
    }
}