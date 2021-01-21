using Microsoft.AspNetCore.Mvc;

namespace App.Web.Controllers.MVC
{
    public class HomeController : Controller
    {
        public ActionResult Error(string msg = "We have an error. Come back later.")
        {
            ViewData["Error"] = msg;
            return View();
        }

        [Route("cookies")]
        public ActionResult Cookies()
        {
            return View();
        }

    }
}