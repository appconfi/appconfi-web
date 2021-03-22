using System.ComponentModel.DataAnnotations;

namespace App.Web.Core.Models.Application
{
    public class NewFeatureToggleModel
    {
        [Required]
        [MaxLength(1000)]
        public string Key { get; set; }
        public bool IsEnabled { get; set; }
        [MaxLength(2000)]
        public string Description { get; set; }
    }
}
