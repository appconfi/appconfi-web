using System.ComponentModel.DataAnnotations;

namespace App.Web.Core.Models.Account
{
    public class ForgotModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
