using Microsoft.AspNetCore.Mvc;
using Unhosted_Api.Data;
using Unhosted_Api.Models;

namespace Unhosted_Api.Controllers;

[ApiController]
[Route("api/iSAuthorized")]
public class CheckController : ControllerBase
{
    private readonly AppDbContext _context;

    public CheckController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
   public ActionResult<bool> Authorized_check(Guid id)
    {
        var user = _context.RegisteredUsers.Find(id);
        if (user == null)
            return NotFound("User is not verified");

        return Ok(user.IsAuthorized);
    }
}