namespace TestWins.Model;

//dotnet add package MySql.Data
using MySql.Data.MySqlClient;

public class ConnectionSql
{
    private readonly string _connectionString = "server=localhost;database=student;uid=root;pwd=root";
        private MySqlConnection _conn;

    public MySqlConnection connectSql()
    {

        Console.WriteLine("Connecting to DB");

        _conn = new MySqlConnection(_connectionString);

        Console.WriteLine(_conn == null ? "Database Connection Failed" : "Database connection successful");

        return _conn;
    }
}