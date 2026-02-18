using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Utlånssystem_Konvensjonell.Core.Domain.Account;
using Utlånssystem_Konvensjonell.Infrastructure.Data;
using Utlånssystem_Konvensjonell.SharedKernel;

namespace Utlånssystem_Konvensjonell.Pages;

public class RegisterModel : PageModel
{
    private readonly BoardGameContext _db;

    public RegisterModel(BoardGameContext db)
    {
        _db = db;
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
        return Page();

    var email = Input.Email.Trim().ToLowerInvariant();

    var exists = await _db.Users.AnyAsync(u => u.Email.ToLower() == email);
    if (exists)
    {
        ModelState.AddModelError("Input.Email", "Email is already registered.");
        return Page();
    }

    var user = new User(Input.Email, Input.Password, Input.FirstName, Input.LastName);


    var validators = new IValidator<User>[]
    {
        new UserFirstNameValidator(),
        new UserLastNameValidator(),
        new UserEmailValidator()
    };

    foreach (var v in validators)
    {
        var (ok, error) = v.IsValid(user);
        if (!ok)
        {
            ModelState.AddModelError(string.Empty, error);
            return Page();
        }
    }

    _db.Users.Add(user);
    await _db.SaveChangesAsync();

    return RedirectToPage("/Login");
}}
