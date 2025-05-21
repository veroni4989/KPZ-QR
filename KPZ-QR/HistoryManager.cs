using Microsoft.Data.Sqlite;
using System.IO;

public class HistoryManager
{
    private static HistoryManager? _instance;
    public static HistoryManager Instance => _instance ??= new HistoryManager();

    private const string DbFile = "history.db";

    private HistoryManager()
    {
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        if (!File.Exists(DbFile))
        {
            using var connection = new SqliteConnection($"Data Source={DbFile}");
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE History (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Text TEXT NOT NULL,
                    Generator TEXT NOT NULL,
                    CreatedAt TEXT NOT NULL
                );";
            command.ExecuteNonQuery();
        }
    }

    public void AddEntry(string text, string generator)
    {
        using var connection = new SqliteConnection($"Data Source={DbFile}");
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO History (Text, Generator, CreatedAt)
            VALUES ($text, $generator, $createdAt);";
        command.Parameters.AddWithValue("$text", text);
        command.Parameters.AddWithValue("$generator", generator ?? "Unknown");
        command.Parameters.AddWithValue("$createdAt", DateTime.Now.ToString("G"));
        command.ExecuteNonQuery();
    }

    public List<string> GetHistory()
    {
        var result = new List<string>();
        using var connection = new SqliteConnection($"Data Source={DbFile}");
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT Text, Generator, CreatedAt FROM History ORDER BY Id DESC;";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            string line = $"{reader["CreatedAt"]} | {reader["Generator"]} | {reader["Text"]}";
            result.Add(line);
        }
        return result;
    }
}
