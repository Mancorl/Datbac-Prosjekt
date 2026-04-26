using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Utlånssystem_Konvensjonell.Infrastructure.Data;
using Utlånssystem_Konvensjonell.Pages;
using System.Diagnostics;

namespace Utlånssystem_Konvensjonell.Pages;

[Authorize(Roles = "Admin")]
public class GameCheckModel : PageModel
{
    private readonly BoardGameContext _db;

    public GameCheckModel(BoardGameContext db)
    {
        _db = db;
    }

    public List<CheckGameList> ReturnedGames { get; set; } = new();

    public async Task OnGetAsync()
    {
        var sw = Stopwatch.StartNew(); 
        ReturnedGames = await _db.Rented
            .Where(b => !b.Active)
            .Join(
                _db.Games,
                b => b.BoardGameId,
                g => g.Id,
                (b, g) => new { Borrow = b, Game = g }
            )
            .Join(
                _db.Users,
                bg => bg.Borrow.UserId,
                u => u.Id,
                (bg, u) => new CheckGameList
                {
                    BorrowId = bg.Borrow.Id,
                    GameId = bg.Game.Id,
                    GameName = bg.Game.Name,
                    Description = bg.Game.Description,
                    ImagePath = bg.Game.ImagePath,
                    UserName = $"{u.First} {u.Last}",
                    UserEmail = u.Email
                }
            )
            .ToListAsync();

            sw.Stop();
            Console.WriteLine($"[Conventional] GameCheck OnGet took {sw.ElapsedMilliseconds} ms");

            MeasurementsLogger.Log("Conventional", "GameCheckOnGet", sw.ElapsedMilliseconds);


    }

    public async Task<IActionResult> OnPostApproveAsync(Guid borrowId)
    {
        var sw = Stopwatch.StartNew(); 
        var borrow = await _db.Rented.FirstOrDefaultAsync(b => b.Id == borrowId);

        if (borrow == null)
            return NotFound();

        var game = await _db.Games.FirstOrDefaultAsync(g => g.Id == borrow.BoardGameId);

        if (game == null)
            return NotFound();

        game.ReturnOne();

        _db.Rented.Remove(borrow);

        await _db.SaveChangesAsync();

        TempData["Message"] = "Game approved and returned to inventory.";

        sw.Stop();
        Console.WriteLine($"[Conventional] ApproveGame OnGet took {sw.ElapsedMilliseconds} ms");

        MeasurementsLogger.Log("Conventional", "ApproveGame", sw.ElapsedMilliseconds);

        return RedirectToPage();
    }
}