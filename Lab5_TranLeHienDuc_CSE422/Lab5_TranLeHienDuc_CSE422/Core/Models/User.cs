using System;
using LibraryManagementSystem.Core.Interfaces;

namespace LibraryManagementSystem.Core.Models
{
    public class User : IObserver
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        public User(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public void Update(string message)
        {
            Console.WriteLine($"Notification to {Name} ({Email}): {message}");
        }
    }
}