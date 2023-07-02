using Microsoft.AspNetCore.Mvc;
using MyPastebin.Data.Models.TextBlockModels;
using MyPastebin.Data.Models.UserModels;
using MyPastebin.Data.Interfaces;
using MyPastebin.Data.Services;
using MyPastebin.Data.Extensions;

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
        var textBlock = await _dbService.GetTextBlockAsync(request.HashId);
        return Ok(textBlock);
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddNewPost([FromBody] CreateTextBlockRequest newPost)
    {
        User? user = null;
        
        if (this.IsUserAuthenticated())
        {
            user = await this.GetCurrentlyAuthenticatedUser(_userService);
        }
        string textBlockHashId = await _dbService.AddTextBlockAsync(newPost, user: user);

        return Ok(new {postId = textBlockHashId});
    }

    [HttpPost]
    [Route("remove")]
    public IActionResult RemovePost(int id)
    {
        throw new NotImplementedException();
    }
}
