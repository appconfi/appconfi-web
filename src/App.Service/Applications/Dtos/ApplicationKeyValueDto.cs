using System;

namespace App.Service.Applications
{
    public class ApplicationKeyValueDto
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        public Guid SettingId { get; set; }
    }
}