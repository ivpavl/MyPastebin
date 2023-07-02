using Microsoft.AspNetCore.Mvc;

namespace MyPastebin.Data.Models.TextBlockModels;

public class GetTextBlockRequest
{
    [FromQuery(Name = "id")]
    public string HashId {get; set;} = null!;

    [FromHeader(Name = "Authorization")]
    public string? AuthToken {get; set;} = null!;
}