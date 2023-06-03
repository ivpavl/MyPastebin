using Microsoft.AspNetCore.Mvc;

namespace MyPastebin.Data.Models.TextBlock;

public class UserModel
{
    public UserModel(string userName)
    {
        UserName = userName;
    }

    public string UserName {get; set;} = null!;
}