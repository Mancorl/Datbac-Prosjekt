using Microsoft.AspNetCore.Mvc;
using Unhosted_Api.Data;
using Unhosted_Api.Models;
using Unhosted_Api.DTO;
using Unhosted_Api.Services;

namespace Unhosted_Api.Controllers;

[ApiController]
[Route("api/BorrowGames")]
public class BorrowBoardGamesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IFileUploadService _fileUploadService;

    public BorrowBoardGamesController(AppDbContext context, IFileUploadService fileUploadService)
    {
        _context = context;
        _fileUploadService = fileUploadService;
    }

    [HttpPost]
public IActionResult Borrow(Guid id)
{

    var game = _context.BoardGames.Find(id);
    if (game == null)
        return NotFound($"That board game was not found.");
    if (game.Quantity <= 0)
        return BadRequest($"That board game is not available for loan.");
    game.Quantity -= 1;

    _context.BoardGames.Update(game);
    _context.SaveChanges();

    return Ok(game);
}
}