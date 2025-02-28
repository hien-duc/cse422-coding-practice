using System.Collections.Generic;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
    public interface ILibraryService
    {
        // Book Management
        void AddBook(IBook book);
        IBook GetBookById(int id);
        IEnumerable<IBook> SearchBooksByTitle(string title);
        IEnumerable<IBook> SearchBooksByCategory(string category);
        
        // Reader Management
        void AddReader(IReader reader);
        IReader GetReaderById(int id);
        
        // Lending Operations
        bool LendBook(int readerId, int bookId);
        bool ReturnBook(int readerId, int bookId);
        bool ReserveBook(int readerId, int bookId);
        void CancelReservation(int readerId, int bookId);
    }
} 