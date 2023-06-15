using MySqlConnector;
using MyPastebin.Data.Interfaces;
using MyPastebin.Data.Models;
using MyPastebin.Data.Models.TextBlockModels;
using MyPastebin.Data.Models.UserModels;

namespace MyPastebin.Data.Services;
public class DbServiceEF : IDataBase
{
    private readonly ApplicationContext _db;
    public DbServiceEF(ApplicationContext db)
    {
        _db = db;
    }

    public async Task<(bool isSuccessful, string postHashId)> AddNewPostAsync(NewTextBlock newPost)
    {
        var hash = GenerateUniqueHash();

        var newTextBlock = new TextBlock()
        {
            Text = newPost.Text,
            HashId = hash,
            User = newPost.User
        };

        _db.TextBlocks.Add(newTextBlock);
        await _db.SaveChangesAsync();

        return (true, hash);
    }

    private string GenerateUniqueHash()
    {
        var r = new Random();
        string newHash;
        do
        {
            newHash = r.Next(1, 100000).ToString();
        } while (_db.TextBlocks.FirstOrDefault(tb => tb.HashId == newHash) is not null);

        return newHash;
    }

    public async Task<(bool isSuccessful, string postTextBlock)> GetPostTextAsync(string postHashId)
    {
        var post = _db.TextBlocks.FirstOrDefault(tb => tb.HashId == postHashId);
        return (post is not null, post?.Text ?? "");
    }
}