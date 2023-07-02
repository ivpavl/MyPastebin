using Microsoft.AspNetCore.Mvc;
using MyPastebin.Data.Exceptions;
using MyPastebin.Data.Interfaces;
using MyPastebin.Data.Models.UserModels;

namespace MyPastebin.Data.Extensions;
public static class ControllerBaseExtensions
{
    public static string UserName(this ControllerBase controllerBase)
    {
        return controllerBase.User.Identity?.Name ?? string.Empty;
    }
    public static bool IsUserAuthenticated(this ControllerBase controllerBase)
    {
        return controllerBase.User.Identity?.IsAuthenticated ?? false;
    }
    public async static Task<User> GetCurrentlyAuthenticatedUser(this ControllerBase controllerBase, IUserService userService)
    {
            var userName = UserName(controllerBase) ?? throw new Exception("Authorized without username in claims");
            return await userService.GetUserAsync(userName) ?? throw new NotFoundException("user name", "username claims");
    }
}