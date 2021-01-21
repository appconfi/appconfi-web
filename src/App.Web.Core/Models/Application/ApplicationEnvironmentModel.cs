using System;

namespace App.Web.Core.Models.Application
{
    public class ApplicationEnvironmentModel
    {
        public Guid ApplicationId { get; set; }

        public Guid EnvironmentId { get; set; }

        public string Name { get; set; }

        public bool IsDefault { get; set; }

    }
}
