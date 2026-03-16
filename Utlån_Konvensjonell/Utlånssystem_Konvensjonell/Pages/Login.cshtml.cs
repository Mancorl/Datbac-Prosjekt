/*
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Utlånssystem_Konvensjonell.Core.Domain.Account;
using Utlånssystem_Konvensjonell.Infrastructure.Data;
using Utlånssystem_Konvensjonell.SharedKernel;
using Utlånssystem_Konvensjonell.Core.Domain.Account.Handlers;
using Utlånssystem_Konvensjonell.Core.Domain.Account.Events;
using Utlånssystem_Konvensjonell.Core.Domain.Account.Services;


namespace Utlånssystem_Konvensjonell.Pages;

public class LoignModel : PageModel
{
    private readonly BoardGameContext _db;

    private readonly LoginService _loginService;
    private readonly LoginUserHandler _handler;

    public LoginModel(
        BoardGameContext db,
        LoginService loginService,
        LoginUserHandler handler)
    {
        _db = db;
        _LoginService = loginService;
        _handler = handler;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public class InputModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = "";


        [Required]
        public string Password { get; set; } = "";
    }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _registrationService.Registered += _handler.OnRegistered;
        await _registrationService.RegisterAsync(Input.Email, Input.Password, Input.FirstName, Input.LastName);

        return RedirectToPage("/Index");
}}
*/


using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Utlånssystem_Konvensjonell.Core.Domain.Account;
using Utlånssystem_Konvensjonell.Infrastructure.Data;
using Utlånssystem_Konvensjonell.SharedKernel;
using Utlånssystem_Konvensjonell.Core.Domain.Account.Handlers;
using Utlånssystem_Konvensjonell.Core.Domain.Account.Events;
using Utlånssystem_Konvensjonell.Core.Domain.Account.Services;

namespace Utlånssystem_Konvensjonell.Pages;

public class LoginModel : PageModel
{
    private readonly LoginService _loginService;

    public LoginModel(LoginService loginService)
    {
        _loginService = loginService;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public class InputModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";
    }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var result = await _loginService.LoginAsync(
            Input.Email,
            Input.Password
        );

        if (!result.Success || result.User == null)
        {
            ModelState.AddModelError(string.Empty, result.Error!);
            return Page();
        }

        Console.WriteLine($"Logga inn: {result.User.First} {result.User.Last}");
        Console.WriteLine($"Useriden: {result.User.Id}");
        Console.WriteLine($"Email: {result.User.Email}");
        Console.WriteLine($"Permission: {result.User.Permission}");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, result.User.Id.ToString()),
            new Claim(ClaimTypes.Name, $"{result.User.First} {result.User.Last}"),
            new Claim(ClaimTypes.Email, result.User.Email)
        };

        if (result.User.Permission == Permission.Admin)
        {
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
        }

        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal);

        return RedirectToPage("/Index");
    }
}