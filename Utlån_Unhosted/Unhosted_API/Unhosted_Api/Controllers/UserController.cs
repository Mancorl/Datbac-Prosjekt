using Microsoft.AspNetCore.Mvc;
using Unhosted_Api.DTO;
using Unhosted_Api.Models;
using Unhosted_Api.Data;
using System.Runtime.InteropServices;
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

    [HttpPost]
    public IActionResult Create([FromBody] UserDto dto)
    {
        if(_context.Users.Any(u => u.Email == dto.Email))
            return BadRequest("Email is already in use.");
        var user = new User(dto.Id, dto.Email, dto.Password, dto.first, dto.last);
        if(!_context.Users.Any())
        {
            user.IsAuthorized = true;
            user.Permission = Permission.Admin;
        }
            
        _context.Users.Add(user);
        _context.SaveChanges();
        
        return Ok(new
        {
            IsAuthorized = user.IsAuthorized,
            Permission = user.Permission
        });
        
    }
}