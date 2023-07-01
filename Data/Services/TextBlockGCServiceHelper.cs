using MyPastebin.Data.Interfaces;
using MyPastebin.Data.Models.TextBlockModels;

namespace MyPastebin.Data.Services;
public class TextblockGarbageCollectorHelper : ITextBlockGCHelper
{   
    private readonly ApplicationContext _db;

    public TextblockGarbageCollectorHelper(ApplicationContext db)
    {
        _db = db;
    }

    public void RemoveExpiredTextBlocks(DateTime currentTime)
    {
        var toBeRemoved = _db.TextBlocks.Where(tb => tb.ExpireIn < currentTime);
        _db.TextBlocks.RemoveRange(toBeRemoved);
        _db.SaveChanges();
    }
}