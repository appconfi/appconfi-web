using App.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace App.Web.Core.Models.Targeting
{
    public class NewTargetingPercentModel
    {
        public Guid? Id { get; set; }
        public Guid EnvironmentId { get; set; }
        public Guid FeatureToggleId { get; set; }
        public int Percent { get; set; }
    }

    public class NewTargetingSpecifiModel
    {
        public Guid? Id { get; set; }
        public Guid EnvironmentId { get; set; }
        public Guid FeatureToggleId { get; set; }
        public TargetOption TargetOption { get; set; }
        [Required]
        [MaxLength(10000)]
        public string List { get; set; }
        [Required]
        [MaxLength(100)]
        public string Property { get; set; }
    }
}
