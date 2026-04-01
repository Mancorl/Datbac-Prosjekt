using Microsoft.AspNetCore.Mvc;
using Unhosted_Api.DTO;
using Unhosted_Api.Models;
using Unhosted_Api.Data;
namespace Unhosted_Api.Controllers;

[ApiController]
[Route("api/User")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;
    public UserController(AppDbContext context)
    {
        _context = context;
    }
    public IActionResult Create([FromBody] UserDto dto)
    {
        if(_context.Users.Any(u => u.Email == dto.Email))
            return BadRequest("Email is already in use.");
        var user = new User(dto.first, dto.last, dto.Email, dto.Password);
        if(!_context.Users.Any())
            user.IsAuthorized = true;
        _context.Users.Add(user);
        _context.SaveChanges();
        return Ok(user);
    }
}