using Microsoft.AspNetCore.Mvc;
using Unhosted_Api.Data;
using Unhosted_Api.Models;
using Unhosted_Api.DTO;
using Unhosted_Api.Services;

namespace Unhosted_Api.Controllers;

[ApiController]
[Route("api/ReturnGames")]
public class ReturnBoardGamesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IFileUploadService _fileUploadService;

    public ReturnBoardGamesController(AppDbContext context, IFileUploadService fileUploadService)
    {
        _context = context;
        _fileUploadService = fileUploadService;
    }

    [HttpPost]
public IActionResult ReturnGame(Guid id)
{

    var game = _context.BoardGames.Find(id);
    if (game == null)
        return NotFound($"That board game was not found.");
    game.Quantity += 1;

    _context.BoardGames.Update(game);
    _context.SaveChanges();

    return Ok(game);
}
}