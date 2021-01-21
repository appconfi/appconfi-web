using System.ComponentModel.DataAnnotations;

namespace App.Web.Core.Models.Application
{
    public class NewApplicationSettingModel
    {
        [Required]
        [MaxLength(1000)]
        public string Key { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Value { get; set; }
    }
}
