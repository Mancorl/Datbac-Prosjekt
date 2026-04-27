using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Utlånssystem_Konvensjonell.Infrastructure.Data;

using Utlånssystem_Konvensjonell.Core.Domain.BoardGames.Handlers;

using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;

using Utlånssystem_Konvensjonell.Core.Domain.Account;

using System.Security.Claims;
using Utlånssystem_Konvensjonell.Core.Domain.Borrowed;
using System.Diagnostics;

namespace Utlånssystem_Konvensjonell.Pages;


[Authorize]
public class LoanModel : PageModel
{
    private readonly BoardGameContext _db;
    private readonly ReturnHandler _returnHandler;

    public LoanModel(BoardGameContext db, ReturnHandler returnHandler)
    {
        _db = db;
        _returnHandler = returnHandler;
    }

 public List<BorrowList> RentedGames { get; set; } = new();

    public async Task OnGetAsync()
    {

        var sw = Stopwatch.StartNew(); 
        

         var userClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userClaim) || !Guid.TryParse(userClaim, out var userId))
        {
            RentedGames = new List<BorrowList>();
            return;
        }
        RentedGames = await _db.Rented
            .Where(b => b.Active && b.UserId == userId)
            .Join(
                _db.Games,
                b => b.BoardGameId,
                g => g.Id,
                (b, g) => new BorrowList
                {
                    BorrowId = b.Id,
                    GameId = g.Id,
                    Name = g.Name,
                    Quantity = g.Quantity,
                    Description = g.Description,
                    ImagePath = g.ImagePath
                }
            )
            .ToListAsync();

            sw.Stop();
            Console.WriteLine($"[Conventional] Load Loans took {sw.ElapsedMilliseconds} ms");

            MeasurementsLogger.Log("Conventional", "Load Loans", sw.ElapsedMilliseconds);
    }

    public async Task<IActionResult> OnPostReturnAsync(Guid BorrowId)
    {
        var sw = Stopwatch.StartNew(); 
        var user = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(user) || !Guid.TryParse(user, out var userId))
            return Forbid();

        try
        {
            await _returnHandler.ReturnAsync(userId, BorrowId);
            TempData["Message"] = "Game returned successfully.";
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.Message;
        }

        sw.Stop();
        Console.WriteLine($"[Conventional] Return Game took {sw.ElapsedMilliseconds} ms");

        MeasurementsLogger.Log("Conventional", "Return Game", sw.ElapsedMilliseconds);

        return RedirectToPage();
    }


    
}