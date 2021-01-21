using System.ComponentModel.DataAnnotations;

namespace App.Web.Core.Models.Users
{
    public class NewInvitationModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
