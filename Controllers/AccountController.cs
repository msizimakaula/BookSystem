using BulkyBook.Models;
using BulkyBook.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BulkyBook.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<Users> signInManager;
        private readonly UserManager<Users> userManager;

        public AccountController(SignInManager<Users> signInManager, UserManager<Users> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;  // store the page user wanted
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // Redirect to the original page if returnUrl exists and is local
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }

                    // Default redirect if no returnUrl
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Email or password is incorrect");
                    ViewData["ReturnUrl"] = returnUrl;
                    return View(model);
                }
            }

            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                Users users = new Users
                {
                    Name = model.Name,
                    Email = model.Email,
                    UserName = model.Email,
                    Role = model.Role,
                };

                var result = await userManager.CreateAsync(users, model.Password);
                if(result.Succeeded)
                {
                    RedirectToAction("Login", "Account");
                }

                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(model);
                }
                
            }

            return View(model);
        }
        public IActionResult VerifyEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>VerifyEmail(VerifyEmail model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError("", "Somethinf is wrong");
                    return View(model);
                }

                else
                {
                    return RedirectToAction("ChangePassword", "Account", new { name = user.Name });
                }

            }
            return View(model);
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
          await  signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");

        }
    }
}
