using Microsoft.AspNetCore.Mvc;
using MyPastebin.Data.Models.TextBlockModels;
using MyPastebin.Data.Models.UserModels;
using MyPastebin.Data.Interfaces;
using MyPastebin.Data.Services;

namespace MyPastebin.Controllers;

[ApiController]
[Route("[controller]")]
public class TextBlockController : ControllerBase
{
    private readonly ITextBlockService _dbService;
    private readonly IUserService _userService;
    public TextBlockController(IUserService userService, ITextBlockService dbService)
    {
        _userService = userService;
        _dbService = dbService;
    }


    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]GetTextBlockRequest request)
    {
        (bool IsSuccessful, TextBlock textBlock) = await _dbService.GetTextBlockAsync(request.HashId);
        if (IsSuccessful)
            return Ok(textBlock);
        
        return NotFound();
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddNewPost([FromBody] PostTextBlockRequest newPost)
    {
        bool postAddingSuccessful;
        string hashId;
        
        if (HttpContext.User?.Identity?.IsAuthenticated ?? false)
        {
            var userName = HttpContext.User.Identity.Name;
            if (!_userService.IsUserExist(userName!, out User existingUser))
            {
                return RedirectToRoute("/logout");
                throw new Exception("User logged in, but could not found in DB");
            }

            (postAddingSuccessful, hashId) = await _dbService.AddTextBlockAsync(newPost, user: existingUser);
        }
        else
        {
            (postAddingSuccessful, hashId) = await _dbService.AddTextBlockAsync(newPost, user: null);
        }

        if (postAddingSuccessful)
        {
            return Ok(new {postId = hashId});
        }

        return BadRequest();
    }

    [HttpPost]
    [Route("remove")]
    public IActionResult RemovePost(int id)
    {
        throw new NotImplementedException();
    }
}
