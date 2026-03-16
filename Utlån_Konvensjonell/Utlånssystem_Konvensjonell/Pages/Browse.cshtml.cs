using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Utlånssystem_Konvensjonell.Infrastructure.Data;
using Utlånssystem_Konvensjonell.Core.Domain.BoardGames;

using Microsoft.AspNetCore.Mvc;

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
}