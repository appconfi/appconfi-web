using App.SharedKernel.Guards;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace App.Web.Controllers.API
{
    public abstract class BaseApiController : Controller
    {
        protected async Task<IActionResult> CreateResponse(Func<Task<IActionResult>> action)
        {
            try
            {
                return await action();
            }
            catch (GuardValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return StatusCode(500, "Internal error");
            }
        }
    }
}
