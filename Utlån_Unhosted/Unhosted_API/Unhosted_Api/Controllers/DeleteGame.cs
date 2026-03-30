using Microsoft.AspNetCore.Mvc;
using Unhosted_Api.Data;
using Unhosted_Api.Models;
using Unhosted_Api.DTO;

namespace Unhosted_Api.Controllers;

[ApiController]
[Route("api/DeleteGame")]
public class DeleteGameController : ControllerBase
{
    private readonly AppDbContext _context;

    public DeleteGameController(AppDbContext context)
    {
        _context = context;
    }

    [HttpDelete("{id}")]
public IActionResult Delete(Guid id)
{
    var game = _context.BoardGames.FirstOrDefault(g => g.Id == id);

    if (game == null)
    {
        return NotFound();
    }

    _context.BoardGames.Remove(game);
    _context.SaveChanges();

    return Ok(game);
}
}