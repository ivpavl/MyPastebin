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

    public (string JwtToken, int MaxAge) TryLoggingIn(AuthUserModel user)
    {
        if(_userService.IsUserExist(userName: user.UserName, out User existingUser))
        {
            if(VerifyPassword(existingUser.HashedPassword, user.Password))
            {
                return CreateJWTToken(user);
            }
            throw new Exception("Wrong password!");
        }
        throw new Exception("User does not exist!");
    }

    public async Task<(string JwtToken, int MaxAge)> TryRegisteringAsync(AuthUserModel user)
    {
        var isUserExist = _userService.IsUserExistAsync(userName: user.UserName);

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
        var newUser = new User()
        {
            UserName = user.UserName,
            UserIp = "userIP",
            HashedPassword = hashedPassword,
        };

        if(!await isUserExist)
        {
            await _userService.AddUserAsync(newUser);
            return CreateJWTToken(user);
        }
        throw new Exception("User already exist!");
    }

    private static bool VerifyPassword(string hashedPassword, string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }

    private static (string Token, int MaxAge) CreateJWTToken(AuthUserModel user)
    {
        var claims = new List<Claim> {new Claim(ClaimTypes.Name, user.UserName) };

        var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromHours(2)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );

        return (new JwtSecurityTokenHandler().WriteToken(jwt), 2*3600);
    }
}