using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Utlånssystem_Konvensjonell.Infrastructure.Data;
using Utlånssystem_Konvensjonell.Core.Domain.BoardGames;

using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;

using Utlånssystem_Konvensjonell.Core.Domain.Account;

namespace Utlånssystem_Konvensjonell.Pages;

public class BrowseModel : PageModel
{
    private readonly BoardGameContext _db;

    public BrowseModel(BoardGameContext db)
    {
        _db = db;
    }

    public List<BoardGame> Games { get; set; } = new();

    public async Task OnGetAsync()
    {
        Games = await _db.Games.ToListAsync();
    }


    public async Task<IActionResult> OnPostRentAsync(Guid id)
    {
        var game = await _db.Games.FirstOrDefaultAsync(g => g.Id == id);

        if (game == null)
            return NotFound();

        if (game.Quantity <= 0)
        {
            TempData["Error"] = "This game is not available.";
            return RedirectToPage();
        }

        await _db.SaveChangesAsync();

        TempData["Message"] = $"You rented {game.Name}.";
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostEditAsync(Guid id)
    {
        return RedirectToPage("/EditGames", new { id = id });
    }

    public async Task<IActionResult> OnPostDeleteAsync(Guid id)
    {

        var game = await _db.Games.FirstOrDefaultAsync(g => g.Id == id);

        if (game == null)
            return NotFound();

        _db.Games.Remove(game);
        await _db.SaveChangesAsync();

        TempData["Message"] = $"{game.Name} was deleted.";
        return RedirectToPage();
    }
}