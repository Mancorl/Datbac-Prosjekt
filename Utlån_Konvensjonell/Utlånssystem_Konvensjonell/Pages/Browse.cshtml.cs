using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Utlånssystem_Konvensjonell.Infrastructure.Data;
using Utlånssystem_Konvensjonell.Core.Domain.BoardGames;
using Utlånssystem_Konvensjonell.Core.Domain.BoardGames.Handlers;

using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;

using Utlånssystem_Konvensjonell.Core.Domain.Account;

using System.Security.Claims;
using Utlånssystem_Konvensjonell.Core.Domain.Borrowed;
using System.Diagnostics;

namespace Utlånssystem_Konvensjonell.Pages;

public class BrowseModel : PageModel
{
    private readonly BoardGameContext _db;
    private readonly BorrowHandler _borrowHandler;

    public BrowseModel(BoardGameContext db, BorrowHandler borrowHandler)
    {
        _db = db;
        _borrowHandler = borrowHandler;
    }

    public List<BoardGame> Games { get; set; } = new();

    public async Task OnGetAsync()
    {
        var sw = Stopwatch.StartNew(); 

        Games = await _db.Games.ToListAsync();

        sw.Stop();
        Console.WriteLine($"[Conventional] Browse OnGet took {sw.ElapsedMilliseconds} ms");

        MeasurementsLogger.Log("Conventional", "BrowseOnGet", sw.ElapsedMilliseconds);
        
    }


    public async Task<IActionResult> OnPostRentAsync(Guid id)
    {
        var sw = Stopwatch.StartNew(); 
        
        try
        {
            var userClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userClaim) || !Guid.TryParse(userClaim, out var userId))
            {
                TempData["Error"] = "You must be logged in to rent games.";
                return RedirectToPage();
            }

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToPage();
            }

            if (!user.IsAuthorized)
            {
                TempData["Error"] = "You have not yet been authorized.";
                return RedirectToPage();
            }

            var gameName = await _borrowHandler.BorrowAsync(userId, id);
            TempData["Message"] = $"You rented {gameName}.";

            return RedirectToPage();
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToPage();
        }
        finally
        {
            sw.Stop();
            Console.WriteLine($"[Conventional] Rent took {sw.ElapsedMilliseconds} ms");
            MeasurementsLogger.Log("Conventional", "Rent", sw.ElapsedMilliseconds);
        }
    }

    public async Task<IActionResult> OnPostEditAsync(Guid id, string name, int quantity, string description, IFormFile? image)
{
    if (!User.IsInRole("Admin"))
        return Forbid();

    var game = await _db.Games.FirstOrDefaultAsync(g => g.Id == id);

    if (game == null)
        return NotFound();

    string? newImagePath = null;

    if (image != null && image.Length > 0)
    {
        var extension = Path.GetExtension(image.FileName).ToLower();

        if (extension != ".png" && extension != ".jpg" && extension != ".jpeg")
        {
            TempData["Error"] = "Invalid image type.";
            return RedirectToPage();
        }

        var fileName = name + extension;
        var imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

        if (!Directory.Exists(imagesFolder))
        {
            Directory.CreateDirectory(imagesFolder);
        }

        var fullPath = Path.Combine(imagesFolder, fileName);

        using (var stream = System.IO.File.Create(fullPath))
        {
            await image.CopyToAsync(stream);
        }

        newImagePath = "images/" + fileName;
    }

    game.Edit(name, quantity, description, newImagePath);

    await _db.SaveChangesAsync();

    TempData["Message"] = $"{game.Name} was updated.";
    return RedirectToPage();
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