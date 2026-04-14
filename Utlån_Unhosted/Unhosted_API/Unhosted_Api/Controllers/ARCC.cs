using Microsoft.AspNetCore.Mvc;
using Unhosted_Api.Data;
using Unhosted_Api.Models;

namespace Unhosted_Api.Controllers;

[ApiController]
[Route("api/AdminReturnCheckController")]
public class AdminReturnCheckController : ControllerBase
{
    private readonly AppDbContext _context;

    public AdminReturnCheckController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Borrowing>> RetrieveBorrows()
    {
        var borrows = _context.Borrow
            .Where(b => !b.Active)
            .ToList();

        return Ok(borrows);
    }
}

