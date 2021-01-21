using App.Service.Accounts;
using App.Service.Common;
using App.SharedKernel.Guards;
using App.Web.Core.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App.Web.Controllers.MVC
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> logger;
        private readonly IAuthService authService;
        private readonly IAccountManager accountManager;

        public AccountController(
            ILogger<AccountController> logger,
            IAuthService authService,
            IAccountManager accountManager)
        {
            this.logger = logger;
            this.authService = authService;
            this.accountManager = accountManager;
        }

        #region Logout

        public async Task<IActionResult> Logout()
        {
            await SignOut();
            return RedirectToAction("index", "admin");
        }

        private async Task SignOut()
        {
            await HttpContext.SignOutAsync();
        }

        #endregion

        #region Login Google

        public IActionResult LoginGoogle(string returnUrl)
        {
            return new ChallengeResult(
                GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action(nameof(LoginGoogleCallback), new { returnUrl })
                });
        }

        public async Task<IActionResult> LoginGoogleCallback(string returnUrl)
        {
            try
            {
                var authenticateResult = await HttpContext.AuthenticateAsync("External");

                if (!authenticateResult.Succeeded)
                    return RedirectToAction(nameof(Login));

                var loginExternal = new LoginExternalDto
                {
                    Email = authenticateResult.Principal.FindFirst(ClaimTypes.Email).Value,
                    FirstName = authenticateResult.Principal.FindFirst(ClaimTypes.GivenName).Value,
                    LastName = authenticateResult.Principal.FindFirst(ClaimTypes.Surname).Value,
                };

                var userid = await accountManager.RegisterExternal(loginExternal);

                await SignIn(loginExternal.Email, userid);
                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Login with Google error");
            }

            return RedirectToAction("index", "admin");
        }

        #endregion

        #region Login

        public IActionResult Login(string returnUrl = null)
        {
            var login = new LoginModel { ReturnUrl = returnUrl };
            return View(login);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel login)
        {
            var autorized = await authService.ValidCredentials(login.Email, login.Password);
            if (!autorized.ValidCredentials)
            {
                ViewData["Error"] = autorized.Error;
                return View(login);
            }

            await SignIn(login.Email, autorized.UserId);

            if (!string.IsNullOrEmpty(login.ReturnUrl))
                return Redirect(login.ReturnUrl);

            return RedirectToAction("Index", "Admin");
        }

        private async Task SignIn(string email, Guid userid)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, "User"),
                new Claim(ClaimTypes.NameIdentifier, userid.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = false,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(10),
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

        }


        #endregion

        #region Register

        [HttpGet]
        public IActionResult Register()
        {
            var register = new RegisterModel { };
            return View(register);
        }


        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await accountManager.RegisterUser(new RegisterUserDto
                {
                    Email = model.Email,
                    Password = model.Password,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                });

            }
            catch (GuardValidationException e)
            {
                ViewData["ModelError"] = e.Message;
                return View(model);
            }

            return RedirectToAction("login", "account");
        }
        #endregion

        #region Verify

        [HttpGet]
        public async Task<IActionResult> Verify(string token)
        {
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("login", "account");

            await accountManager.Verify(token);

            return RedirectToAction("login", "account");
        }

        #endregion

        #region Forgot

        public IActionResult Forgot()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Forgot(ForgotModel model)
        {
            await accountManager.ForgotPassword(model.Email);
            ViewData["Send"] = true;
            return View();
        }

        #endregion

        #region Reset

        public IActionResult Reset([FromQuery] string token)
        {
            if (string.IsNullOrEmpty(token))
                return BadRequest();

            ViewData["Token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Reset(ResetPasswordModel model)
        {
            await accountManager.ResetPassword(new ResetPasswordDto
            {
                Password = model.Password,
                Token = model.Token
            });

            ViewData["Changed"] = true;
            return View();
        }

        #endregion
    }
}