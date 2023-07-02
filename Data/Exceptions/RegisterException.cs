
namespace MyPastebin.Data.Exceptions;
public class RegisterException : Exception
{
    public RegisterException(string message)
        : base(message) { }
}