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

    public BorrowBoardGamesController(AppDbContext context)
    {
        _context = context;
    }


    [HttpPost]
    public IActionResult Borrow([FromBody] BorrowDto dto)
    {
        var game = _context.BoardGames.Find(dto.GameId);
        if (game == null)
            return NotFound("That board game was not found.");

        if (game.Quantity <= 0)
            return BadRequest("That board game is not available for loan.");

        /*var reguser = _context.RegisteredUsers.Find(dto.UserId);
        if (reguser == null){
            return NotFound("That user was not found.");
        }else   if (!BCrypt.Net.BCrypt.Verify(dto.Password, reguser.Password) || !BCrypt.Net.BCrypt.Verify(dto.Email, reguser.Hashid)) {
            Console.WriteLine($"DTO password: {dto.Password}");
            Console.WriteLine($"DB password: {reguser.Password}");
            Console.WriteLine($"Password hash length: {reguser.Password?.Length}");
            Console.WriteLine(!BCrypt.Net.BCrypt.Verify(dto.Password, reguser.Password));


            Console.WriteLine($"DTO Email: {dto.Email}");
            Console.WriteLine($"DB Hash: {reguser.Hashid}");
            Console.WriteLine($"Email hash length: {reguser.Hashid?.Length}");
            Console.WriteLine(!BCrypt.Net.BCrypt.Verify(dto.Email, reguser.Hashid));
            return BadRequest("Incorrect password or email.");
        }*/
        var reguser = _context.RegisteredUsers.Find(dto.UserId);
        if (reguser == null)
        {
            return NotFound("That user was not found.");
        }

        var passwordOk = BCrypt.Net.BCrypt.Verify(dto.Password, reguser.Password);
        var emailOk = BCrypt.Net.BCrypt.Verify(dto.Email, reguser.Hashid);

        Console.WriteLine("[BORROW]");
        Console.WriteLine($"DTO UserId: {dto.UserId}");
        Console.WriteLine($"Loaded reguser.Id: {reguser.Id}");
        Console.WriteLine($"DTO Password: '{dto.Password}'");
        Console.WriteLine($"DTO Password Length: {dto.Password?.Length}");
        Console.WriteLine($"Stored Password Hash: '{reguser.Password}'");
        Console.WriteLine($"Password OK: {passwordOk}");

        Console.WriteLine($"DTO Email: '{dto.Email}'");
        Console.WriteLine($"DTO Email Length: {dto.Email?.Length}");
        Console.WriteLine($"Stored Email Hash: '{reguser.Hashid}'");
        Console.WriteLine($"Email OK: {emailOk}");

        if (!passwordOk || !emailOk)
        {
            return BadRequest("Incorrect password or email.");
        }
        
        

        var existingBorrow = _context.Borrow.FirstOrDefault(b =>
            b.UserId == dto.UserId &&
            b.BoardGameId == dto.GameId &&
            b.Active);

        if (existingBorrow != null)
            return BadRequest("User already has an active borrowing for this game.");

        game.Quantity -= 1;

        var borrowing = new Borrowing(dto.UserId, dto.GameId, dto.Email, true);

        _context.BoardGames.Update(game);
        _context.Borrow.Add(borrowing);
        _context.SaveChanges();

        var result = new BorrowDto
        {
            Id = borrowing.Id,
            UserId = borrowing.UserId ?? Guid.Empty,
            GameId = borrowing.BoardGameId ?? Guid.Empty,
            Active = borrowing.Active
        };

        return Ok(result);
    }


    [HttpGet("user/{userId}")]
public IActionResult GetUserBorrowings(Guid userId)
{
    var borrowings = _context.Borrow
        .Where(b => b.UserId == userId)
        .ToList();

    var result = borrowings.Select(b => new BorrowDto
    {
        Id = b.Id,
        UserId = b.UserId ?? Guid.Empty,
        GameId = b.BoardGameId ?? Guid.Empty,
        Email = b.Email,
        Active = b.Active
    });

    return Ok(result);
}
}