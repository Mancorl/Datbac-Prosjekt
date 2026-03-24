using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Utlånssystem_Konvensjonell.Core.Domain.Account;
using Utlånssystem_Konvensjonell.Infrastructure.Data;
using Utlånssystem_Konvensjonell.SharedKernel;


namespace Utlånssystem_Konvensjonell.Pages;

public class IndexModel : PageModel
{
    
    private readonly BoardGameContext _db;

    public IndexModel(BoardGameContext db)
    {
        _db = db;
    }

    public bool isauthorized {get;set;}

    public async Task OnGetAsync()
    {

        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdString))
            return;

        if (!Guid.TryParse(userIdString, out var userId))
            return;

        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user != null)
        {
            isauthorized = user.IsAuthorized;
        }

    }

}
