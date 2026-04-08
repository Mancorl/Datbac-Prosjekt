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

    public ReturnBoardGamesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
public IActionResult ReturnGame([FromBody] BorrowDto dto)
{

    var game = _context.BoardGames.Find(dto.GameId);
    if (game == null)
        return NotFound($"That board game was not found.");
    game.Quantity += 1;

    var retgame = _context.Borrow.Find(dto.UserId);
    if (retgame == null)
        return NotFound($"Desync error: User not found.");
    retgame.Active = false;

    _context.BoardGames.Update(game);
    _context.Borrow.Update(retgame);
    _context.SaveChanges();

    return Ok(game);
}
}