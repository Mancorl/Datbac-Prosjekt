using Microsoft.AspNetCore.Mvc;
using Unhosted_Api.Data;
using Unhosted_Api.Models;

namespace Unhosted_Api.Controllers;

[ApiController]
[Route("api/GetUnauthorizedController")]
public class GetUnauthorizedController : ControllerBase
{
    private readonly AppDbContext _context;

    public GetUnauthorizedController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> RetrieveBoardGames()
    {
        var userlist = _context.Users.ToList();
        userlist.RemoveAt(0);
        if (userlist.Count == 0)
            return NotFound("No unverified users found.");
        return Ok(userlist);
    }
}