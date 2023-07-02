 
namespace MyPastebin.Data.Models.UserModels;

public class AuthUserModel
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
public User ToUser(string hashedPassword)
{
    return new User()
    {
        UserName = UserName,
        UserIp = "userIP",
        HashedPassword = hashedPassword,
    };
}
}