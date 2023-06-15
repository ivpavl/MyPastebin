using MyPastebin.Data.Interfaces;
using MyPastebin.Data.Models.TextBlockModels;
using MyPastebin.Data.Models.UserModels;

namespace MyPastebin.Data.Services;
public class UserService : IUserService
{
    private readonly ApplicationContext _db;
    public UserService(ApplicationContext db)
    {
        _db = db;
    }

    public async Task<(bool isSuccessful, User createdUser)> AddUserAsync(User newUser)
    {
        try
        {
            await _db.Users.AddAsync(newUser); 
            await _db.SaveChangesAsync();
        }
        catch
        {
            throw;
            // return (false, null)!;
        }
        return (true, newUser);
    }

    public async Task<User> EnsureUserCreated(string userName)
    {
        if(!IsUserExist(userName: userName, out User user))
        {
            var newUser = new User()
            {
                UserName = userName,
                UserIp = ""
            };
            (bool isAddingUserSuccessful, user) = await AddUserAsync(newUser);
            if(!isAddingUserSuccessful)
                throw new Exception("Unable to create user!");
        }
        return user;
    }
    public async Task<bool> AddUserPassword(User user, string hashedUserPassword)
    {
        if(user.HashedPassword is null)
        {
            user.HashedPassword = hashedUserPassword;
            await _db.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<(bool isSuccessful, User postOwner)> AddUserToTextblock(string userName, string postHashId) 
    {
        User user = await EnsureUserCreated(userName: userName);

        var post = _db.TextBlocks.FirstOrDefault(tb => tb.HashId == postHashId);
        if(post is null)
            throw new Exception("Post was created and has HashID, but was failed to append to user");
        post.User = user;

        await _db.SaveChangesAsync();
        return (true, user);
    }

    public bool IsUserExist(string userName, out User existingUser)
    {
        User user = _db.Users.FirstOrDefault(u => u.UserName == userName)!;
        existingUser = user;
        return user is not null;
    }

}
