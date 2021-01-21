using System.ComponentModel.DataAnnotations;

namespace App.Web.Core.Models.Application
{
    public class NewApplicationFeatureToggleModel
    {
        [Required]
        [MaxLength(1000)]
        public string Key { get; set; }

        public bool IsEnabled { get; set; }
    }
}
