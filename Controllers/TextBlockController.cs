using Microsoft.AspNetCore.Mvc;
using MyPastebin.Data.Models.TextBlockModels;
using MyPastebin.Data.Models.UserModels;
using MyPastebin.Data.Interfaces;

namespace MyPastebin.Controllers;

[ApiController]
[Route("[controller]")]
public class TextBlockController : ControllerBase
{
    private readonly IDataBase _dbService;
    private readonly IUserService _userService;
    public TextBlockController(IUserService userService, IDataBase dbService)
    {
        _userService = userService;
        _dbService = dbService;
    }


    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]GetTextBlockRequest request)
    {
        (bool IsSuccessful, string textBlock) = await _dbService.GetPostTextAsync(request.HashId);
        if (IsSuccessful)
            return new JsonResult(new {TextBlock = textBlock});

        return new JsonResult(new {TextBlock = "Nonetext"});
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddNewPost([FromBody] PostTextBlockRequest newPost)
    {
        User user = await _userService.EnsureUserCreated(userName: newPost.UserName);

        var textBlockToAdd = new NewTextBlock(
            userName: newPost.UserName, 
            text: newPost.TextBlock,
            user: user
        );

        (bool postAddingSuccessful, string hashId) = await _dbService.AddNewPostAsync(textBlockToAdd);
        
        if(postAddingSuccessful)
        {
            return Ok(hashId);
        }

        return StatusCode(StatusCodes.Status400BadRequest);
    }

    [HttpPost]
    [Route("remove")]
    public IActionResult RemovePost(int id)
    {
        throw new NotImplementedException();
    }
}
