using Microsoft.AspNetCore.Mvc;
using Unhosted_Api.Data;
using Unhosted_Api.Models;
using Unhosted_Api.DTO;
using Unhosted_Api.Services;

namespace Unhosted_Api.Controllers;

[ApiController]
[Route("api/EditGames")]
public class EditBoardGamesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IFileUploadService _fileUploadService;

    public EditBoardGamesController(AppDbContext context, IFileUploadService fileUploadService)
    {
        _context = context;
        _fileUploadService = fileUploadService;
    }

    [HttpPost]
public IActionResult Create([FromForm] AdminBoardGameDto dto)
{

    var game = _context.BoardGames.Find(dto.Id);
    if (game == null)
        return NotFound($"Board game with Id {dto.Id} not found.");
    if (dto.Image != null)
{
    // Upload the new image and update the path
    game.ImagePath = _fileUploadService.UploadImage(dto.Image);
}
    game.Name = dto.Name;
    game.Quantity = dto.Quantity;
    game.Description = dto.Description;
    game.Loanable = dto.Loanable;

    _context.BoardGames.Update(game);
    _context.SaveChanges();

    return Ok(game);
}
}