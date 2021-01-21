using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Controllers.MVC
{
    [Authorize]
    public abstract class BaseController : Controller
    {

    }
}