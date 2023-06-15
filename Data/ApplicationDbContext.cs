using Microsoft.EntityFrameworkCore;
using MyPastebin.Data.Models.TextBlockModels; 
using MyPastebin.Data.Models.UserModels; 

namespace MyPastebin.Data;
public class ApplicationContext : DbContext
{
    public DbSet<TextBlock> TextBlocks { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
 
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();

    }
}