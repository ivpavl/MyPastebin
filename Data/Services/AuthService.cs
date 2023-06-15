using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using MyPastebin.Data.Interfaces;
using MyPastebin.Data.Models.UserModels;
using MyPastebin.Data.Static;
using Microsoft.IdentityModel.Tokens;

namespace MyPastebin.Data.Services;
public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    public AuthService(IUserService userService)
    {
        _userService = userService;
    }

    public (bool IsSuccessful, string jwtToken) TryLoggingIn(AuthUserModel user)
    {
        if(_userService.IsUserExist(userName: user.UserName, out User existingUser))
        {
            if(existingUser.HashedPassword is null)
            {
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
                _userService.AddUserPassword(existingUser, hashedPassword);
                return (true, CreateJWTToken(user));
            }
            else
            {
                if(VerifyPassword(existingUser.HashedPassword, user.Password))
                {
                    return (true, CreateJWTToken(user));
                }
            }
        }
        return (false, "");
    }

    public (bool IsSuccessful, string jwtToken) TryRegistering(AuthUserModel user)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
        var newUser = new User()
        {
            UserName = user.UserName,
            UserIp = "userIP",
            HashedPassword = hashedPassword,
        };

        if(!_userService.IsUserExist(userName: user.UserName, out User existingUser))
        {
            _userService.AddUserAsync(newUser);
            return TryLoggingIn(user);
        }
        return (false, "");
    }

    private bool VerifyPassword(string hashedPassword, string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }

    private string CreateJWTToken(AuthUserModel user)
    {
        var claims = new List<Claim> {new Claim(ClaimTypes.Name, user.UserName) };

        var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromHours(2)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}