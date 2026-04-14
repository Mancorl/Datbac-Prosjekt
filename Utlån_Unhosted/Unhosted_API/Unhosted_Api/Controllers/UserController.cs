using Microsoft.AspNetCore.Mvc;
using Unhosted_Api.DTO;
using Unhosted_Api.Models;
using Unhosted_Api.Data;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using System;

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
        Console.WriteLine("[CREATE USER]");
        Console.WriteLine($"DB Source: {_context.Database.GetDbConnection().DataSource}");
        Console.WriteLine($"User.Id: {user.Id}");
        Console.WriteLine($"Plain password from DTO: '{dto.Password}'");
        Console.WriteLine($"Stored hash: '{user.Password}'");
        Console.WriteLine($"Verify immediately: {BCrypt.Net.BCrypt.Verify(dto.Password, user.Password)}");
        if(!_context.Users.Any())
        {
            user.IsAuthorized = true;
            user.Permission = Permission.Admin;
        }
            
        _context.Users.Add(user);
        _context.SaveChanges();

        var savedUser = _context.Users.Find(user.Id);

        Console.WriteLine("[AFTER SAVE]");
        Console.WriteLine($"DB Source: {_context.Database.GetDbConnection().DataSource}");
        Console.WriteLine($"Saved User.Id: {savedUser.Id}");
        Console.WriteLine($"Saved User.Email: {savedUser.Email}");
        Console.WriteLine($"Saved User.Password: {savedUser.Password}");
        Console.WriteLine($"Verify after save: {BCrypt.Net.BCrypt.Verify(dto.Password, savedUser.Password)}");
        
        return Ok(new
        {
            IsAuthorized = user.IsAuthorized,
            Permission = user.Permission
        });
        
    }
}