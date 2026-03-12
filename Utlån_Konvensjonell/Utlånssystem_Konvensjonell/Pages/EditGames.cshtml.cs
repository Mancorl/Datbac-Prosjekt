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

public class EditGameModel : PageModel
{
private readonly BoardGameContext _db;
private readonly AddGameHandler _AddGameHandler;

public RegisterModel(
        BoardGameContext db,
        AddGameHandler AddGameHandler)
    {
        _db = db;
        _AddGameHandler = AddGameHandler;
    }



[BindProperty]
    public InputGameModel Input { get; set; } = new();

    public class InputGameModel
    {
        [Required]
        public string GameTitle { get; set; } = "";

        [Required]
        public int Quantity { get; set; } = 1;

        [Required]
        public bool Loanable { get; set; } = true;

    }

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