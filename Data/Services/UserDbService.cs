using MySqlConnector;
using MyPastebin.Data.Interfaces;
using MyPastebin.Data.Models;

namespace MyPastebin.Data.Services;
public class UserDbService : IUserDb
{
    private readonly MySqlConnection _userDbConnection;
    public UserDbService()
    {
        string connectionString = "server=localhost;database=MyPastebinUsers;user=root;password=12345678;";
        _userDbConnection = new MySqlConnection(connectionString);
        _userDbConnection.Open();
        InitDb();
    }

    public async Task AddUser(CreatePost newPost)
    {

    }

    private async void InitDb()
    {
        Console.WriteLine("Initing db!");
        var myTrans = _userDbConnection.BeginTransaction();
        var command = _userDbConnection.CreateCommand();

        command.Transaction = myTrans;
        command.Connection = _userDbConnection;

        command.CommandText = @"DROP Table users;";
        command.ExecuteNonQuery();
        command.CommandText = @"CREATE Table users (
            Id INT AUTO_INCREMENT PRIMARY KEY,
            Username TEXT,
            UserIP TEXT
        );
        ";
        command.ExecuteNonQuery();
        command.CommandText = @"INSERT INTO users (username, userip) VALUES ('User1', '127.1.1.77');";
        command.ExecuteNonQuery();

        myTrans.Commit();
    }

    ~UserDbService()
    {
        _userDbConnection.Close();
    }
}