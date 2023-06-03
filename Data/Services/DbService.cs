using MySqlConnector;
using MyPastebin.Data.Interfaces;
using MyPastebin.Data.Models;
using MyPastebin.Data.Models.TextBlock;

namespace MyPastebin.Data.Services;
public class DbService : IDataBase
{
    private readonly MySqlConnection _db;
    public DbService()
    {
        string connectionString = "server=localhost;database=MyPastebinUsers;user=root;password=12345678;";
        _db = new MySqlConnection(connectionString);
        _db.Open();
        // InitUsers();
        // InitTextblocks();
    }

    public async Task<bool> AddUserAsync(UserModel newUser)
    {
        string queryString =
            "INSERT INTO users (UserName) "
                + "VALUES (@UserName)";

        using var command = new MySqlCommand(queryString, _db);
        command.Parameters.AddWithValue("UserName", newUser.UserName);

        var result = await command.ExecuteScalarAsync();
    
        return true;
    }
    private async Task<(bool isSuccessful, int userId)> IsUserExistAsync(UserModel user)
    {
        string queryString =
            "SELECT Id FROM users "
                + "WHERE UserName = @UserName";

        using var command = new MySqlCommand(queryString, _db);
        command.Parameters.AddWithValue("UserName", user.UserName);

        using var reader = await command.ExecuteReaderAsync();
        reader.Read();

        bool isSuccessful = reader.HasRows;
        int userId = isSuccessful ? Convert.ToInt32(reader.GetValue(0)) : 0;

        return (isSuccessful, userId);
    }

    public async Task<(bool isSuccessful, string postHashId)> AddNewPostAsync(NewTextBlock newPost)
    {
        string queryString =
        "INSERT INTO textblocks (Text, OwnerId, HashId) "
            + "VALUES (@Text, @UserId, @HashId)";

        var userModel = new UserModel(newPost.UserName);
        (bool userExist, int userId) = await IsUserExistAsync(userModel);
        if(!userExist)
        {
            await AddUserAsync(userModel);
            (userExist, userId) = await IsUserExistAsync(userModel);
            if(!userExist)
                throw new Exception("After adding user to DB, user still does not exist");
        }

        var hash = GenerateUniqueHash();

        using var command = new MySqlCommand(queryString, _db);
        command.Parameters.AddWithValue("Text", newPost.Text);
        command.Parameters.AddWithValue("UserId", userId);
        command.Parameters.AddWithValue("HashId", hash);

        await command.ExecuteScalarAsync();

        return (true, hash); 
    }

    private string GenerateUniqueHash()
    {
        var r = new Random();
        return r.Next(1, 100000).ToString();
    }
    
    public async Task<(bool isSuccessful, string postTextBlock)> GetPostTextAsync(string postHashId)
    {
        string queryString =
            "SELECT Text from textblocks "
                + "WHERE HashId = @postId";

        using var command = new MySqlCommand(queryString, _db);
        command.Parameters.AddWithValue("postId", postHashId);

        using var reader = await command.ExecuteReaderAsync();
        reader.Read();

        bool isSuccessful = reader.HasRows;
        string postTextBlock = isSuccessful ? reader.GetValue(0).ToString()! : "";

        return (isSuccessful, postTextBlock);
    }


    private async void EnsureDbCreated()
    {
        throw new NotImplementedException();
    }
    private async void InitUsers()
    {
        Console.WriteLine("Initing db!");
        var myTrans = _db.BeginTransaction();
        var command = _db.CreateCommand();

        command.Transaction = myTrans;
        command.Connection = _db;

        // command.CommandText = @"DROP Table users;";
        // command.ExecuteNonQuery();
        command.CommandText = @"CREATE Table users (
            Id INT AUTO_INCREMENT PRIMARY KEY,
            Username TEXT,
            UserIP TEXT
        );
        ";
        command.ExecuteNonQuery();
        command.CommandText = @"INSERT INTO users (username, userip) VALUES ('User1', '127.1.1.77');";
        command.ExecuteNonQuery();

        await myTrans.CommitAsync();
    }

    private async void InitTextblocks()
    {
        Console.WriteLine("Initing textblocks!");
        var myTrans = _db.BeginTransaction();
        var command = _db.CreateCommand();

        command.Transaction = myTrans;
        command.Connection = _db;

        // command.CommandText = @"DROP Table textblocks;";
        // command.ExecuteNonQuery();
        command.CommandText = @"CREATE Table textblocks (
            Id INT AUTO_INCREMENT PRIMARY KEY,
            Text TEXT,
            OwnerId INT,
            FOREIGN KEY (OwnerId) REFERENCES users (Id) ON DELETE CASCADE
        );
        ";
        command.ExecuteNonQuery();
        command.CommandText = @"INSERT INTO textblocks (Text, OwnerId) VALUES ('Lorem fdfsfdsf', 1);";
        command.ExecuteNonQuery();

        await myTrans.CommitAsync();
    }

    ~DbService()
    {
        _db.Close();
    }
}