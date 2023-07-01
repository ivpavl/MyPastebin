using MyPastebin.Data.Models.UserModels;

namespace MyPastebin.Data.Interfaces;
public interface ITextBlockGCHelper
{
    void RemoveExpiredTextBlocks(DateTime currentTime);
}