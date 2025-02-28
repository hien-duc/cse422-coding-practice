using System;

namespace LibraryManagementSystem.Infrastructure.Data
{
    public sealed class DatabaseConnection
    {
        private static readonly Lazy<DatabaseConnection> _instance = 
            new Lazy<DatabaseConnection>(() => new DatabaseConnection());

        public static DatabaseConnection Instance => _instance.Value;

        private DatabaseConnection()
        {
            Console.WriteLine("Database connection initialized.");

        }

        public void ExecuteQuery(string query)
        {
            Console.WriteLine($"Executing query: {query}");
        }
    }
}