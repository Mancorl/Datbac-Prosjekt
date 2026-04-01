using Microsoft.AspNetCore.Mvc;
using Unhosted_Api.Data;
using Unhosted_Api.Models;
using Unhosted_Api.DTO;
using Unhosted_Api.Services;

namespace Unhosted_Api.Controllers;

[ApiController]
[Route("")]
public class HelloController : ControllerBase
{


    [HttpGet]
public IActionResult HelloWorld()
{
    return Ok("Hello world");
}
}