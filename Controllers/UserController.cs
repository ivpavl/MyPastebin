using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MyPastebin.Data.Models.UserModels;
using MyPastebin.Data.Interfaces;


namespace MyPastebin.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IAuthService _auth;
    public UserController(IAuthService auth)
    {
        _auth = auth;
    }
 

    [HttpGet]
    [Authorize]
    [Route("")]
    public IActionResult GetUserInfo()
    {
        return Ok(HttpContext?.User?.Identity?.Name ?? "None");
    }

    [HttpPost]
    [Route("login")]
    public IActionResult Login([FromBody]AuthUserModel user)
    {
        (bool isSuccessful, string jwtToken) = _auth.TryLoggingIn(user);
        if(isSuccessful)
            return Ok(new {JWTToken = jwtToken});

        return BadRequest();
    }

    [HttpPost]
    [Route("register")]
    public IActionResult Register([FromBody]AuthUserModel user)
    {
        (bool isSuccessful, string jwtToken) = _auth.TryRegistering(user);
        if(isSuccessful)
            return Ok(new {JWTToken = jwtToken});

        return BadRequest();
    }

    [HttpPost]
    [Route("logout")]
    public IActionResult Logout()
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
