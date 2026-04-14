using Unhosted_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Unhosted_Api.Data;
using Microsoft.EntityFrameworkCore;
namespace Unhosted_Api.Controllers;
[ApiController]
[Route("api/RegisterUser")]
public class RegistrationController : ControllerBase
{
    private readonly AppDbContext _context;
    public RegistrationController(AppDbContext context)
    {
        _context = context;
    }
    [HttpPost("{id}")]
    public IActionResult Register(bool reg, Guid id)
    {
        if (reg)
        {
            User user=_context.Users.Find(id);
            Console.WriteLine($"[REGISTER]");
            Console.WriteLine($"DB Source: {_context.Database.GetDbConnection().DataSource}");
            Console.WriteLine($"Requested Id: {id}");
            Console.WriteLine($"User.Id: {user.Id}");
            Console.WriteLine($"User.Email: {user.Email}");
            Console.WriteLine($"User.Password: {user.Password}");
            Console.WriteLine($"Verify user password against 'Mjausa': {BCrypt.Net.BCrypt.Verify("Mjausa", user.Password)}");
            if(user == null)
                return NotFound("User not found.");
            RegisteredUser reguser = new RegisteredUser(user.Id,user.Email, user.Password);
            Console.WriteLine($"RegUser.Password: {reguser.Password}");
            Console.WriteLine($"Same reference: {user.Password == reguser.Password}");
            
            _context.RegisteredUsers.Add(reguser);
            _context.Users.Remove(user);
            _context.SaveChanges(); 

            var hash = BCrypt.Net.BCrypt.HashPassword("hoho123");
            Console.WriteLine("Testen idkkkk:");
            Console.WriteLine(hash);
            Console.WriteLine(BCrypt.Net.BCrypt.Verify("hoho123", hash)); // MUST be true
            return Ok(reguser);

            
            
        }else
        {
            RegisteredUser reguser = _context.RegisteredUsers.Find(id);
            if(reguser == null)
                return NotFound("Registered user not found.");
            _context.RegisteredUsers.Remove(reguser);
            _context.SaveChanges();
            return Ok("User removed");
        }
    }
}