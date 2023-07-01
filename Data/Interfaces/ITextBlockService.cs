using MyPastebin.Data.Models;
using MyPastebin.Data.Models.TextBlockModels;
using MyPastebin.Data.Models.UserModels;

namespace MyPastebin.Data.Interfaces;
public interface ITextBlockService
{
    Task<(bool isSuccessful, TextBlock textBlock)> GetTextBlockAsync(string postHashId);
    Task<(bool isSuccessful, string postHashId)> AddTextBlockAsync(PostTextBlockRequest newPost, User? user);
    IEnumerable<TextBlock> GetUserPosts(User user, int limitTextByChars = 20);

    
}