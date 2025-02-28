using LibraryManagementSystem.Models;
using System.Collections.Generic;

namespace LibraryManagementSystem.Repositories
{
    public interface IBookRepository
    {
        IBook GetBookById(int id);
        IEnumerable<IBook> GetBooksByTitle(string title);
        IEnumerable<IBook> GetBooksByCategory(string category);
        void AddBook(IBook book);
        void UpdateBook(IBook book);
        bool DeleteBook(int id);
        void InitializeTestData();
    }
} 