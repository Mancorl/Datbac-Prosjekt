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
public IActionResult Create([FromForm] CreateBoardGameDto dto)
{
    var imagePath = _fileUploadService.UploadImage(dto.Image);
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