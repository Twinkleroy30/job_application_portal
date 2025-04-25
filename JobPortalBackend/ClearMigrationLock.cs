using System;
using Microsoft.Data.Sqlite;

namespace JobPortalBackend
{
    class ClearMigrationLock
    {
        static void Main(string[] args)
        {
            var connectionString = "Data Source=jobportal.db";
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM __EFMigrationsLock WHERE Id = 1;";
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"Deleted {rowsAffected} rows from __EFMigrationsLock.");
            }
        }
    }
}
