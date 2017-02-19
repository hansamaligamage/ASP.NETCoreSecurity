using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using IdentityServer4.Services;
using tokenserver.ViewModels;
using Microsoft.AspNetCore.Http.Authentication;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace tokenserver.Controllers
{
    public class AccountController : Controller
    {

        private UserManager<IdentityUser> _userManager;
        private IIdentityServerInteractionService _interaction;

        public AccountController(UserManager<IdentityUser> userManager, IIdentityServerInteractionService interaction)
        {
            _userManager = userManager;
            _interaction = interaction;
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var identityUser = await _userManager.FindByNameAsync(loginViewModel.UserName);

                if (identityUser != null
                    && await _userManager.CheckPasswordAsync(identityUser, loginViewModel.Password))
                {
                    AuthenticationProperties properties = null;
                    if (loginViewModel.RememberMe)
                    {
                        properties = new AuthenticationProperties
                        {
                            IsPersistent = true
                        };
                    }

                    await HttpContext.Authentication.SignInAsync(identityUser.Id, identityUser.UserName);

                    if (_interaction.IsValidReturnUrl(loginViewModel.ReturnUrl))
                        return Redirect(loginViewModel.ReturnUrl);

                    return Redirect("~/");
                }

                ModelState.AddModelError("", "Invalid username or password.");
            }

            return View(loginViewModel);
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View();
        }


    }
}
