using Microsoft.AspNetCore.Mvc;
using Unhosted_Api.Data;
using Unhosted_Api.Models;
using Unhosted_Api.DTO;

namespace Unhosted_Api.Controllers;

[ApiController]
[Route("api/AddGames")]
public class AddBoardGamesController : ControllerBase
{
    private readonly AppDbContext _context;

    public AddBoardGamesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
public IActionResult Create(CreateBoardGameDto dto)
{
    var imagePath = string.IsNullOrWhiteSpace(dto.ImagePath)
        ? "images/Default.jpg"
        : dto.ImagePath;
    var game = new BoardGame(
        dto.Name,
        dto.Quantity,
        true,
        imagePath,
        dto.Description
    );

    _context.BoardGames.Add(game);
    _context.SaveChanges();

    return Ok(game);
}
}