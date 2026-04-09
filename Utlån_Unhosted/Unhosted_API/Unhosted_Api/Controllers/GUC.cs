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
    public ActionResult<IEnumerable<User>> RetrieveUCC()
    {
        var userlist = _context.Users.ToList();

        if (userlist.Count == 0)
            return Ok(new List<User>());

        userlist.RemoveAt(0);

        return Ok(userlist);
    }
}