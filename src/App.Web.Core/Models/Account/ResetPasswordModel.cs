using System.ComponentModel.DataAnnotations;

namespace App.Web.Core.Models.Account
{
    public class ResetPasswordModel : ViewModel
    {
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
