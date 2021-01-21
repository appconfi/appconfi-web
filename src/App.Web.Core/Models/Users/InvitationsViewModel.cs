using App.Domain;
using System.Collections.Generic;

namespace App.Web.Core.Models.Users
{
    public class InvitationsViewModel
    {
        public IEnumerable<Invitation> Invitations { get; set; }
        public IEnumerable<UserApplication> UsersApplications { get; set; }
    }
}
