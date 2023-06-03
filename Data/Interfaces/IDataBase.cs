using MyPastebin.Data.Models;
using MyPastebin.Data.Models.TextBlock;

namespace MyPastebin.Data.Interfaces;
public interface IDataBase
{
    Task<bool> AddUserAsync(UserModel newUser);
    Task<(bool isSuccessful, string postTextBlock)> GetPostTextAsync(string postHashId);
    Task<(bool isSuccessful, string postHashId)> AddNewPostAsync(NewTextBlock newPost);
    
}