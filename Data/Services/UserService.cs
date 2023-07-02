using MyPastebin.Data.Interfaces;
using MyPastebin.Data.Models.TextBlockModels;
using MyPastebin.Data.Models.UserModels;
using Microsoft.EntityFrameworkCore;

namespace MyPastebin.Data.Services;
public class UserService : IUserService
{
    private readonly ApplicationContext _db;
    public UserService(ApplicationContext db)
    {
        _db = db;
    }

    public async Task<User> AddUserAsync(User newUser)
    {
        await _db.Users.AddAsync(newUser); 
        await _db.SaveChangesAsync();
        return newUser;
    }
    public bool IsUserExist(string userName, out User existingUser)
    {
        User? user = _db.Users.FirstOrDefault(u => u.UserName == userName)!;
        existingUser = user;
        return user is not null;
    }
    public async Task<bool> IsUserExistAsync(string userName)
    {
        User? user = await _db.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        return user is not null;
    }
    public async Task<User?> GetUserAsync(string userName)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        return user;
    }


    // public async Task<bool> AddUserPasswordAsync(User user, string hashedUserPassword)
    // {
    //     if(user.HashedPassword is null)
    //     {
    //         user.HashedPassword = hashedUserPassword;
    //         await _db.SaveChangesAsync();
    //         return true;
    //     }
    //     return false;
    // }
    // public async Task<User> EnsureUserCreatedAsync(string userName)
    // {
    //     if(!IsUserExist(userName: userName, out User user))
    //     {
    //         var newUser = new User()
    //         {
    //             UserName = userName,
    //             UserIp = ""
    //         };
    //         (bool isAddingUserSuccessful, user) = await AddUserAsync(newUser);
    //         if(!isAddingUserSuccessful)
    //             throw new Exception("Unable to create user!");
    //     }
    //     return user;
    // }
}
