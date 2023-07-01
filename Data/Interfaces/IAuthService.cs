using MyPastebin.Data.Models.UserModels;

namespace MyPastebin.Data.Interfaces;
public interface IAuthService
{
    // Task<(bool IsSuccessful, string jwtToken)> TryLoggingInAsync(AuthUserModel user);
    (string JwtToken, int MaxAge) TryLoggingIn(AuthUserModel user);
    Task<(string JwtToken, int MaxAge)> TryRegisteringAsync(AuthUserModel user);
    
}