using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.MVC.Controllers;

public class UsersController : Controller
{
    private readonly IAuthenticationService authService;

    public UsersController(IAuthenticationService authService)
    {
        this.authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM login, string returnUrl)
    {
        if (ModelState.IsValid)
        {
            returnUrl ??= Url.Content("~/");
            var isLoggedIn = await authService.Authenticate(login.Email, login.Password);
            if (isLoggedIn) return LocalRedirect(returnUrl);
        }
        ModelState.AddModelError("", "Log In Attempt Failed. Please try again.");
        return View(login);
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM registration)
    {
        if (ModelState.IsValid)
        {
            var returnUrl = Url.Content("~/");
            var isCreated = await authService.Register(registration);
            if (isCreated) return LocalRedirect(returnUrl);
        }

        ModelState.AddModelError("", "Registration Attempt Failed. Please try again.");
        return View(registration);
    }

    [HttpPost]
    public async Task<IActionResult> Logout(string returnUrl)
    {
        returnUrl ??= Url.Content("~/");
        await authService.Logout();
        return LocalRedirect(returnUrl);
    }
}
