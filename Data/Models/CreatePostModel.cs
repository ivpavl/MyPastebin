
namespace MyPastebin.Data.Models;

public class CreatePost
{
    public int Id {get; set;}
    public string TextBlock {get; set;} = null!;
    public string IpAddress {get; set;} = null!;
}