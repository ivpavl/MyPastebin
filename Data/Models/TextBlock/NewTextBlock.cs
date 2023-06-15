using Microsoft.AspNetCore.Mvc;
using MyPastebin.Data.Models.UserModels;

namespace MyPastebin.Data.Models.TextBlockModels;

public class NewTextBlock
{
    public NewTextBlock(string userName, string text, User user)
    {
        UserName = userName;
        Text = text;
        User = user;
    }
    public string UserName {get; set;} = null!;
    public string Text {get; set;} = null!;
    public User User {get; set;} = null!;
}