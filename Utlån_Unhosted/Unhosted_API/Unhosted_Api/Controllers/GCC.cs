using Microsoft.AspNetCore.Mvc;
using Unhosted_Api.Data;
using Unhosted_Api.Models;
using Unhosted_Api.DTO;

namespace Unhosted_Api.Controllers;

[ApiController]
[Route("api/GameCheckerController")]
public class GameCheckedController : ControllerBase
{
    private readonly AppDbContext _context;

    public GameCheckedController(AppDbContext context)
    {
        _context = context;
    }

    [HttpDelete("{id}")]
public IActionResult DeleteBorrow(Guid id)
{
    var game = _context.Borrow.FirstOrDefault(g => g.Id == id);

    if (game == null)
    {
        return NotFound();
    }

    _context.Borrow.Remove(game);
    _context.SaveChanges();

    return Ok(game);
}
}