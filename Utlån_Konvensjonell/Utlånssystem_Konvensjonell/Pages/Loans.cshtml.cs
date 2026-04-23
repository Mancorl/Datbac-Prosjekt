using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Utlånssystem_Konvensjonell.Infrastructure.Data;

using Utlånssystem_Konvensjonell.Core.Domain.BoardGames.Handlers;

using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;

using Utlånssystem_Konvensjonell.Core.Domain.Account;

using System.Security.Claims;
using Utlånssystem_Konvensjonell.Core.Domain.Borrowed;

namespace Utlånssystem_Konvensjonell.Pages;

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
        RentedGames = await _db.Rented
            .Where(b => b.Active)
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
    }

    public async Task<IActionResult> OnPostReturnAsync(Guid gameId)
    {
        var user = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(user) || !Guid.TryParse(user, out var userId))
            return Forbid();

        try
        {
            await _returnHandler.ReturnAsync(userId, gameId);
            TempData["Message"] = "Game returned successfully.";
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToPage();
    }


    
}