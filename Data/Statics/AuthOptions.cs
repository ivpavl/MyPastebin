using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MyPastebin.Data.Static;
public class AuthOptions
{
    public const string ISSUER = "MyPastebin";
    public const string AUDIENCE = "Client";
    private const string KEY = "1234098756Key!12321321345555";
    public static SymmetricSecurityKey GetSymmetricSecurityKey () => new(Encoding.UTF8.GetBytes(KEY));
}
