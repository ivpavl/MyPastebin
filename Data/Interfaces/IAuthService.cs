using MyPastebin.Data.Models.UserModels;

namespace MyPastebin.Data.Interfaces;
public interface IAuthService
{
    (bool IsSuccessful, string jwtToken) TryLoggingIn(AuthUserModel user);
    (bool IsSuccessful, string jwtToken) TryRegistering(AuthUserModel user);
    
}