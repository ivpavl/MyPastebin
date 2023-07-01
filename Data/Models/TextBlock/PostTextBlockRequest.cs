using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyPastebin.Data.Models.UserModels;

namespace MyPastebin.Data.Models.TextBlockModels;

public class PostTextBlockRequest
{
    public string TextBlock {get; set;} = null!;
    public string Title {get; set;} = null!;
    public TextBlockExpiration ExpireIn {get; set;} = TextBlockExpiration.OneDay;
}
