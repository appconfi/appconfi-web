using System.ComponentModel.DataAnnotations;

namespace App.Web.Core.Models.Account
{
    public class RegisterModel : ViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        public string LastName { get; set; }


        [Required(ErrorMessage = "You have to agree the terms and conditions")]
        public bool? Accept { get; set; }
    }
}
