using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Service.Applications
{
    public interface IInvitationService
    {
        Task InviteUser(string email, Guid applicationId);

        Task<IEnumerable<Domain.Invitation>> GetPending(Guid applicationId);

        Task DeleteInvitation(Guid invitationId, Guid applicationId);

        Task RemovePermissions(Guid userId, Guid applicationId);

        Task<IEnumerable<Domain.UserApplication>> GetApproved(Guid applicationId);
    }
}