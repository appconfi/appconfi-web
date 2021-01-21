using System;
using System.ComponentModel.DataAnnotations;

namespace App.Web.Core.Models.Application
{
    public class EditApplicationSettingModel
    {
        public Guid SettingId { get; set; }

        public Guid EnvironmentId { get; set; }

        public string EnvironmentName { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Value { get; set; }
    }
}
