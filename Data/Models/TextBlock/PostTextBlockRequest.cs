using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyPastebin.Data.Models.UserModels;

namespace MyPastebin.Data.Models.TextBlockModels;

public class CreateTextBlockRequest
{
    public string TextBlock {get; set;} = null!;
    public string Title {get; set;} = null!;
    public TextBlockExpiration ExpireIn {get; set;} = TextBlockExpiration.OneDay;

    public TextBlock ToTextBlock(string postHashId, User? user, DateTime? expireIn)
    {
        var newTextBlock = new TextBlock()
        {
            Title = Title,
            Text = TextBlock,
            HashId = postHashId,
            User = user,
            ExpireIn = expireIn,
        };

        return newTextBlock;
    }
}
