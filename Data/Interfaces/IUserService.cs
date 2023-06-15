using MyPastebin.Data.Models.UserModels;

namespace MyPastebin.Data.Interfaces;

public interface IUserService
{
    Task<(bool isSuccessful, User createdUser)> AddUserAsync(User newUser);
    Task<(bool isSuccessful, User postOwner)> AddUserToTextblock(string userName, string postHashId);
    Task<User> EnsureUserCreated(string userName);
    bool IsUserExist(string userName, out User existingUser);
    Task<bool> AddUserPassword(User user, string hashedUserPassword);


}