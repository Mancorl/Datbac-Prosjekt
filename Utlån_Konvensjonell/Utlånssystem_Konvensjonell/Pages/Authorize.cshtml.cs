using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Utlånssystem_Konvensjonell.Core.Domain.Account;
using Utlånssystem_Konvensjonell.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc; 
using System.Diagnostics;


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
        var sw = Stopwatch.StartNew(); 

        Users = await _db.Users.Where(u => !u.IsAuthorized)
        .ToListAsync();

        sw.Stop();
        Console.WriteLine($"[Conventional] Authorize OnGet took {sw.ElapsedMilliseconds} ms");

        MeasurementsLogger.Log("Conventional", "AuthorizeOnGet", sw.ElapsedMilliseconds);
    }

    public async Task<IActionResult> OnPostAuthorizeAsync(Guid id)
    {
        var sw = Stopwatch.StartNew(); 

        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
            return NotFound();

        user.Authorize();
        await _db.SaveChangesAsync();

        sw.Stop();
        Console.WriteLine($"[Conventional] Authorize User took {sw.ElapsedMilliseconds} ms");

        MeasurementsLogger.Log("Conventional", "Authorize user", sw.ElapsedMilliseconds);


        return RedirectToPage();
    }
}