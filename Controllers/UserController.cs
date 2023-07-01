using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MyPastebin.Data.Models.UserModels;
using MyPastebin.Data.Interfaces;


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
        var userName = (HttpContext?.User?.Identity?.Name) ?? throw new Exception("Authorized without username in claims");
        var user = await _userService.GetUserAsync(userName) ?? throw new Exception("Authorized, but user not found");

        // Mapper service?
        // Mapper service?

        var response = new UserInfoModel()
        {
            UserName = user.UserName,
            UserIp = user.UserIp,
        };

        // Mapper service?
        // Mapper service?

        return Ok(response);
    }

    [HttpPost]
    [Route("login")]
    public IActionResult Login([FromBody]AuthUserModel user)
    {
        (string token, int maxAge) = _authService.TryLoggingIn(user);
        // if(token != string.Empty)
        return Ok(new {Token = token, MaxAge = maxAge});

        // return BadRequest();
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody]AuthUserModel user)
    {
        (string token, int maxAge) = await _authService.TryRegisteringAsync(user);
        // if(token != string.Empty)
        return Ok(new {Token = token, MaxAge = maxAge});

        // return BadRequest();
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
    public IActionResult GetPostList()
    {
        var userName = HttpContext?.User?.Identity?.Name ?? "";
        if(_userService.IsUserExist(userName, out User user))
        {
            var postsList = _dbService.GetUserPosts(user);
            return Ok(postsList);
        }
        throw new Exception("User logged in, but could not found in DB");
    }
}
