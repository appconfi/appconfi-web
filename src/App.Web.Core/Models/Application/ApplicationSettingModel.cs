using System;

namespace App.Web.Core.Models.Application
{
    public class ApplicationSettingModel
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        /// <summary>
        /// Setting id
        /// </summary>
        public Guid Id { get; set; }
    }
}
