using MyPastebin.Data.Models.TextBlockModels;

namespace MyPastebin.Data.Models.UserModels;

public class User
{
    public int Id {get; set;}
    public string UserName {get; set;} = null!;
    public string UserIp {get; set;} = null!;
    public string? HashedPassword {get; set;} = null!;
    public List<TextBlock> UsersTextBlocks {get; set;} = new();

}