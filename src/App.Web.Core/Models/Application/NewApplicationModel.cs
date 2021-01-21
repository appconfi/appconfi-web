using System.ComponentModel.DataAnnotations;

namespace App.Web.Core.Models.Application
{
    public class NewApplicationModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
