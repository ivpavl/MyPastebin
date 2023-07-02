using MyPastebin.Data.Interfaces;
using MyPastebin.Data.Models.TextBlockModels;
using MyPastebin.Data.Models.UserModels;
using Microsoft.EntityFrameworkCore;
using MyPastebin.Data.Exceptions;

namespace MyPastebin.Data.Services;
public class TextBlockService : ITextBlockService
{
    private readonly ApplicationContext _db;

    public TextBlockService(ApplicationContext db)
    {
        _db = db;
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

    public async Task<TextBlock> GetTextBlockAsync(string postHashId)
    {
        var textBlock = await _db.TextBlocks.FirstOrDefaultAsync(tb => tb.HashId == postHashId);

        if(textBlock is null || DateTime.Now > textBlock?.ExpireIn)
        {
            throw new NotFoundException(nameof(TextBlock), postHashId);
        }

        return textBlock!;
    }
    public async Task<string> AddTextBlockAsync(CreateTextBlockRequest newPost, User? user)
    {
        if(user is not null)
        {
            _db.Attach(user);
        }

        var hash = GenerateUniqueHash();

        var newTextBlock = newPost.ToTextBlock(hash, user, DateTimeFactory(newPost.ExpireIn));

        await _db.TextBlocks.AddAsync(newTextBlock);
        await _db.SaveChangesAsync();

        return hash;
    }

    public IEnumerable<TextBlock> GetUserPosts(User user, int limitTextByChars = 0)
    {
        var textBlocks = _db.TextBlocks.Where(tb => tb.User != null && tb.User.Id == user.Id);
        foreach (var block in textBlocks)
        {
            if(limitTextByChars != 0 && block.Text.Length > limitTextByChars) block.Text = block.Text[..limitTextByChars];
            block.Title ??= "Untitled";
        }
        return textBlocks;
    }

    private static DateTime? DateTimeFactory(TextBlockExpiration expireIn)
    {
        DateTime date = DateTime.Now;
        return expireIn switch
        {
            TextBlockExpiration.OneMinute => date.AddMinutes(1),
            TextBlockExpiration.TenMinutes => date.AddMinutes(10),
            TextBlockExpiration.OneHour => date.AddHours(1),
            TextBlockExpiration.OneDay => date.AddDays(1),
            TextBlockExpiration.OneMonth => date.AddMonths(1),
            TextBlockExpiration.NEVER => null,
            _ => date.AddHours(1),
        };
    }
}