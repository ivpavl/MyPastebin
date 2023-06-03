using Microsoft.AspNetCore.Mvc;
using MyPastebin.Data.Models.TextBlock;
using MyPastebin.Data.Interfaces;
namespace MyPastebin.Controllers;

[ApiController]
[Route("[controller]")]
public class TextBlockController : ControllerBase
{
    private readonly IDataBase _dbService;
    public TextBlockController(IDataBase dbService)
    {
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
        var textBlockToAdd = new NewTextBlock(newPost.UserName, text: newPost.TextBlock);
        (bool IsSuccessful, string hashId) = await _dbService.AddNewPostAsync(textBlockToAdd);
        
        if(IsSuccessful)
            return Ok(hashId);

        return StatusCode(StatusCodes.Status400BadRequest);
    }

    [HttpPost]
    [Route("remove")]
    public IActionResult RemovePost(int id)
    {
        throw new NotImplementedException();
    }
}
