using App.Domain.ActivityLogs;
using App.Service.Accounts;
using App.Service.Common;
using App.SharedKernel.Exceptions;
using App.SharedKernel.Guards;
using App.SharedKernel.Repository;
using App.SharedKernel.Specifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Service.Applications
{
    public class InvitationService : IInvitationService
    {
        private readonly IHasApplicationPermissionPolicy hasApplicationPermission;
        private readonly IEmailService emailService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IAuthService authService;
        private readonly IUrlService urlService;
        private readonly IActivityService activityService;

        public InvitationService(
            IHasApplicationPermissionPolicy hasApplicationPermission,
            IEmailService emailService,
            IUnitOfWork unitOfWork,
            IAuthService authService,
            IUrlService urlService,
            IActivityService activityService)
        {
            this.hasApplicationPermission = hasApplicationPermission;
            this.emailService = emailService;
            this.unitOfWork = unitOfWork;
            this.authService = authService;
            this.urlService = urlService;
            this.activityService = activityService;
        }

        public async Task<IEnumerable<Domain.Invitation>> GetPending(Guid applicationId)
        {
            var userId = authService.CurrentUserId();
            Guard.IsTrue(await hasApplicationPermission.ToRead(userId, applicationId), new UnauthorizedException());

            var invitations = unitOfWork.Repository<Domain.Invitation, Guid>();
            return await invitations.GetAsync(Domain.Invitation.WithApplication(applicationId));
        }

        public async Task<IEnumerable<Domain.UserApplication>> GetApproved(Guid applicationId)
        {
            var userId = authService.CurrentUserId();
            Guard.IsTrue(await hasApplicationPermission.ToRead(userId, applicationId), new UnauthorizedException());

            var applications = unitOfWork.Repository<Domain.UserApplication, int>();
            return await applications.GetAsync(Domain.UserApplication.WithApplication(applicationId), "User.Account");
        }

        public async Task DeleteInvitation(Guid invitationId, Guid applicationId)
        {
            var userId = authService.CurrentUserId();
            Guard.IsTrue(await hasApplicationPermission.ToWrite(userId, applicationId), new UnauthorizedException());


            var invitations = unitOfWork.Repository<Domain.Invitation, Guid>();
            var invitation = await invitations.FirstOrDefaultAsync(Domain.Invitation.WithId(invitationId).And(Domain.Invitation.WithApplication(applicationId)));

            Guard.IsNotNull(invitation, "Invalid invitation");
            invitations.Delete(invitation);
            await unitOfWork.SaveAsync();
        }

        public async Task RemovePermissions(Guid userId, Guid applicationId)
        {
            var currentUserId = authService.CurrentUserId();
            Guard.IsTrue(await hasApplicationPermission.ToWrite(currentUserId, applicationId), new UnauthorizedException());

            var applications = unitOfWork.Repository<Domain.Application, Guid>();
            var application = await applications.GetById(applicationId, "Users");
            Guard.IsNotNull(application, "Invalid operation");

            application.RevokePermissionForUser(userId);

            await unitOfWork.SaveAsync();
        }

        public async Task InviteUser(string email, Guid applicationId)
        {
            var user = await authService.CurrentUser();

            Guard.IsTrue(await hasApplicationPermission.ToWrite(user.Id, applicationId), new UnauthorizedException());

            var invitations = unitOfWork.Repository<Domain.Invitation, Guid>();
            var users = unitOfWork.Repository<Domain.User, Guid>();

            var anyInvitationForThisEmail = await invitations.AnyAsync(Domain.Invitation.WithEmail(email).And(Domain.Invitation.WithApplication(applicationId)));
            Guard.IsFalse(anyInvitationForThisEmail, "You already invite this user");

            var applications = unitOfWork.Repository<Domain.Application, Guid>();
            var application = await applications.GetById(applicationId);

            var invitationUrl = urlService.GetBaseUrl();


            //Check if the user exits
            var invitedUser = await users.FirstOrDefaultAsync(new DirectSpecification<Domain.User>(x => x.Account.Email == email), "Account");
            if (invitedUser != null)
            {
                application.GrantPermissionForUser(invitedUser, Domain.ApplicationPermissions.Admin);
                invitationUrl += $"/account/login";
            }
            else
            {
                var invitation = Domain.Invitation.NewInvitation(email, application);
                invitations.Insert(invitation);
                invitationUrl += $"/account/register?i={invitation.Token}&email={email}";
            }

            activityService.Log(
               LogString.WithName("Invitation", "sent"),
               LogString.WithDescription(new Dictionary<string, string> { { "Email", email } }), applicationId, user.Id);

            await unitOfWork.SaveAsync();
            await emailService.SendEmailAsync(new InvitationEmail(application.Name, invitationUrl, user.FullName) { To = email, Subject = $"{user.FullName} has invited you to join {application.Name}" });
        }

    }
}
