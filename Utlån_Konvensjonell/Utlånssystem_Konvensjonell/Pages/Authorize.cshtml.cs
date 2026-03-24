using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Utlånssystem_Konvensjonell.Core.Domain.Account;
using Utlånssystem_Konvensjonell.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc; 


namespace Utlånssystem_Konvensjonell.Pages;

[Authorize(Roles = "Admin")]
public class AuthorizeUserModel : PageModel
{
    private readonly BoardGameContext _db;

    public AuthorizeUserModel(BoardGameContext db)
    {
        _db = db;
    }

    public List<User> Users { get; set; } = new();

    public async Task OnGetAsync()
    {
        Users = await _db.Users.Where(u => !u.IsAuthorized)
        .ToListAsync();
    }

    public async Task<IActionResult> OnPostAuthorizeAsync(Guid id)
    {

        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
            return NotFound();

        user.Authorize();
        await _db.SaveChangesAsync();

        //TempData["Message"] = $"{game.Name} was deleted.";
        return RedirectToPage();
    }
}