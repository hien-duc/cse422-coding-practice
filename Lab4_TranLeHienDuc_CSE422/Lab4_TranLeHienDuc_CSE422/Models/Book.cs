using System;

namespace LibraryManagementSystem.Models
{
    public class Book : IBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public string BookType { get; set; } = "Physical";  // Default to physical book
    }
} 