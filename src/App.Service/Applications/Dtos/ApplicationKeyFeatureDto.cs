using System;

namespace App.Service.Applications
{
    public class ApplicationKeyFeatureDto
    {
        public string Key { get; set; }

        public bool IsEnabled { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        public Guid SettingId { get; set; }
    }
}