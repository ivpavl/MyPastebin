using Microsoft.AspNetCore.Mvc;
using MyPastebin.Data.Models;

namespace MyPastebin.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    public UserController()
    {
    }

    [HttpGet]
    [Route("/")]
    public IActionResult GetUserInfo()
    {
        throw new NotImplementedException();
    }
    [HttpPost]
    [Route("login")]
    public IActionResult Login()
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [Route("logout")]
    public IActionResult Logout()
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [Route("register")]
    public IActionResult Register()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("postlist")]
    public IActionResult GetPostList()
    {
        throw new NotImplementedException();
    }
}
