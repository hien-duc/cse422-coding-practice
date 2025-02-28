using System.Collections.Generic;

namespace LibraryManagementSystem.Models
{
    public interface IReader
    {
        int Id { get; set; }
        string Name { get; set; }
        List<IBook> BorrowedBooks { get; set; }
        int MaxBorrowedBooks { get; }
        bool CanBorrowBooks => BorrowedBooks.Count < MaxBorrowedBooks;
    }
} 