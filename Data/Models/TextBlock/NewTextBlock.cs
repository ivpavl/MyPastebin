using Microsoft.AspNetCore.Mvc;

namespace MyPastebin.Data.Models.TextBlock;

public class NewTextBlock
{
    public NewTextBlock(string userName, string text)
    {
        UserName = userName;
        Text = text;
    }
    public string UserName {get; set;} = null!;
    public string Text {get; set;} = null!;
}