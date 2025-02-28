using System.Collections.Generic;

namespace LibraryManagementSystem.Models
{
    public class Reader : IReader
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<IBook> BorrowedBooks { get; set; } = new List<IBook>();
        public int MaxBorrowedBooks => 3;
    }
} 