using MyPastebin.Data.Models;
using MyPastebin.Data.Models.TextBlockModels;

namespace MyPastebin.Data.Interfaces;
public interface IDataBase
{
    Task<(bool isSuccessful, string postTextBlock)> GetPostTextAsync(string postHashId);
    Task<(bool isSuccessful, string postHashId)> AddNewPostAsync(NewTextBlock newPost);
    
}