
using MyPastebin.Data.Models;

namespace MyPastebin.Data.Interfaces;
public interface IUserDb
{
    Task AddUser(CreatePost newPost);
}