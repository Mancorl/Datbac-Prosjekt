using Microsoft.AspNetCore.Mvc;
using Unhosted_Api.Services;
namespace Unhosted_Api.Controllers;

[ApiController]
[Route("")]
public class HomeController : ControllerBase
{
    private readonly HelloService _service;

    public HomeController(HelloService service)
    {
        _service = service;
    }

    [HttpGet]
    public string Get() => _service.GetMessage();

}