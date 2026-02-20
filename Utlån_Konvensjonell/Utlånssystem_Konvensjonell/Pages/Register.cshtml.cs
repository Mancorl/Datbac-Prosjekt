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

public class RegisterModel : PageModel
{
    private readonly BoardGameContext _db;

    public RegisterModel(BoardGameContext db)
    {
        _db = db;
    }
    private readonly RegistrationService _registrationService;
    private readonly RegisterUserHandler _handler;

    public RegisterModel(
        BoardGameContext db,
        RegistrationService registrationService,
        RegisterUserHandler handler)
    {
        _db = db;
        _registrationService = registrationService;
        _handler = handler;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public class InputModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        public string FirstName { get; set; } = "";

        [Required]
        public string LastName { get; set; } = "";

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
