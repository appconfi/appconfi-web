using System;

namespace App.Service.Applications
{
    public class ApplicationEnvKeyFeatureDto
    {
        public string EnvironmentName { get; set; }

        public bool IsEnabled { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        public Guid EnvironmentId { get; set; }
    }
}