using App.Service.Applications;
using App.SharedKernel.Exceptions;
using App.Web.Core.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace App.Web.Controllers.MVC
{
    [Authorize]
    public class UsersController : BaseController
    {
        private readonly IInvitationService invitationService;

        public UsersController(IInvitationService invitationService)
        {
            this.invitationService = invitationService;
        }

        public async Task<IActionResult> View([FromQuery] Guid applicationId)
        {
            ViewData["ApplicationId"] = applicationId;

            var model = new InvitationsViewModel
            {
                Invitations = await invitationService.GetPending(applicationId),
                UsersApplications = await invitationService.GetApproved(applicationId)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Invite([FromQuery] Guid applicationId, [FromForm] NewInvitationModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await invitationService.InviteUser(model.Email, applicationId);
                }
                catch (AppException e)
                {
                    return RedirectToAction("view", "users", new { applicationId, error = e.Message });
                }
            }
            return RedirectToAction("view", "users", new { applicationId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteInvitation([FromForm] Guid id, [FromQueryAttribute] Guid applicationId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await invitationService.DeleteInvitation(id, applicationId);
                }
                catch (AppException e)
                {
                    return RedirectToAction("view", "users", new { applicationId, error = e.Message });
                }
            }
            return RedirectToAction("view", "users", new { applicationId });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAccess([FromForm] Guid id, [FromQueryAttribute] Guid applicationId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await invitationService.RemovePermissions(id, applicationId);
                }
                catch (AppException e)
                {
                    return RedirectToAction("view", "users", new { applicationId, error = e.Message });
                }
            }
            return RedirectToAction("view", "users", new { applicationId });
        }
    }
}