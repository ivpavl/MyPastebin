using Microsoft.AspNetCore.Mvc;
using MyPastebin.Data.Interfaces;
using MyPastebin.Data.Models;

namespace MyPastebin.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly IUserDb _test;
    public PostController(IUserDb test)
    {
        _test = test;
    }


    public class MockPost
    {
        public int Id {get; set;} = 25;
        public string TextBlock {get; set;} = "Lorem ipsum sir d...";
    }

    [HttpGet]
    public IActionResult Get()
    {
        // Check if this textblock exist
        // Return to user
        return Ok(new MockPost());
    }

    [HttpPost]
    [Route("add")]
    public IActionResult Post(CreatePost postInfo)
    {
        // Add to textblock(MongoDB, or use filesystem) database
        // Add to Users(MySQL) database

        return Ok();
    }
}
