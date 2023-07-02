using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MyPastebin.Data.Models.UserModels;
using MyPastebin.Data.Interfaces;
using MyPastebin.Data.Extensions;


namespace MyPastebin.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IAuthService _authService;
     private readonly ITextBlockService _dbService;
    private readonly IUserService _userService;
    public UserController(IAuthService auth, IUserService userService, ITextBlockService dbService)
    {
        _authService = auth;
        _userService = userService;
        _dbService = dbService;
    }

    [HttpGet]
    [Authorize]
    [Route("")]
    public async Task<IActionResult> GetUserInfo()
    {
        var user = await this.GetCurrentlyAuthenticatedUser(_userService);
        var response = user.ToUserInfoModel();
        return Ok(response);
    }

    [HttpPost]
    [Route("login")]
    public IActionResult Login([FromBody]AuthUserModel user)
    {
        (string token, int maxAge) = _authService.TryLoggingIn(user);
        return Ok(new {Token = token, MaxAge = maxAge});
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody]AuthUserModel user)
    {
        (string token, int maxAge) = await _authService.TryRegisteringAsync(user);
        return Ok(new {Token = token, MaxAge = maxAge});
    }

    [HttpPost]
    [Route("logout")]
    public IActionResult Logout()
    {
        throw new NotImplementedException();
    }


    [HttpGet]
    [Authorize]
    [Route("postlist")]
    public async Task<IActionResult> GetPostList()
    {
        var user = await this.GetCurrentlyAuthenticatedUser(_userService);
        var postsList = _dbService.GetUserPosts(user);
        return Ok(postsList);
    }
}
