using Microsoft.AspNetCore.Mvc;
using Unhosted_Api.Data;
using Unhosted_Api.Models;

namespace Unhosted_Api.Controllers;

[ApiController]
[Route("api/RetrieveGames")]
public class RetrieveBoardGamesController : ControllerBase
{
    private readonly AppDbContext _context;

    public RetrieveBoardGamesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/RetrieveGames
    [HttpGet]
    public ActionResult<IEnumerable<BoardGame>> RetrieveBoardGames()
    {
        var BoardGames = _context.BoardGames.ToList();
        return Ok(BoardGames); // returns JSON automatically
    }
}