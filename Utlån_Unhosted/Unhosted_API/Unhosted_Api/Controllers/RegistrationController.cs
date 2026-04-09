using Unhosted_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Unhosted_Api.Data;
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
            if(user == null)
                return NotFound("User not found.");
            RegisteredUser reguser = new RegisteredUser(user.Id,user.Email, user.Password);
            _context.RegisteredUsers.Add(reguser);
            _context.Users.Remove(user);
            _context.SaveChanges(); 
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