using System.ComponentModel.DataAnnotations;
using MyPastebin.Data.Models.UserModels;

namespace MyPastebin.Data.Models.TextBlockModels;
public class TextBlock
{
    [Key]
    public int Id {get; set;}
    public string? Title {get; set;} = null!;
    public string Text {get; set;} = null!;
    public string HashId {get; set;} = null!;
    // public TextBlockExpiration ExpireIn {get; set;} = TextBlockExpiration.OneDay;
    public DateTime? ExpireIn {get; set;}
    public User? User {get; set;}
}