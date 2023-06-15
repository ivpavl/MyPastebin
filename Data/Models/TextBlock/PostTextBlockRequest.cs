using Microsoft.AspNetCore.Mvc;

namespace MyPastebin.Data.Models.TextBlockModels;

public class PostTextBlockRequest
{
    public string TextBlock {get; set;} = null!;
    public string UserName {get; set;} = null!;
}