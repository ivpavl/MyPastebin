using MyPastebin.Data.Models.TextBlockModels;
using MyPastebin.Data.Models.UserModels;

namespace MyPastebin.Data.Interfaces;

public interface IUserService
{
    Task<(bool isSuccessful, User createdUser)> AddUserAsync(User newUser);
    Task<bool> IsUserExistAsync(string userName);
    bool IsUserExist(string userName, out User existingUser);
    Task<User?> GetUserAsync(string userName);
    // Task<User> GetUserAsync(int userId);
    // Task<bool> AddUserPassword(User user, string hashedUserPassword);
    // Task<User> EnsureUserCreatedAsync(string userName);
    // Task AddUserToTextBlockAsync(string userName, TextBlock textBlock);
    // bool IsUserExist(string userName);

}