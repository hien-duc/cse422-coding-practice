using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IReaderRepository _readerRepository;

        public LibraryService(IBookRepository bookRepository, IReaderRepository readerRepository)
        {
            _bookRepository = bookRepository;
            _readerRepository = readerRepository;
        }

        public void AddBook(IBook book)
        {
            _bookRepository.AddBook(book);
            Console.WriteLine($"Added book: {book.Title}");
        }

        public IBook GetBookById(int id)
        {
            return _bookRepository.GetBookById(id);
        }

        public IEnumerable<IBook> SearchBooksByTitle(string title)
        {
            return _bookRepository.GetBooksByTitle(title);
        }

        public IEnumerable<IBook> SearchBooksByCategory(string category)
        {
            return _bookRepository.GetBooksByCategory(category);
        }

        public void AddReader(IReader reader)
        {
            _readerRepository.AddReader(reader);
            Console.WriteLine($"Reader added: {reader.Name}");
        }

        public IReader GetReaderById(int id)
        {
            return _readerRepository.GetReaderById(id);
        }

        public bool LendBook(int readerId, int bookId)
        {
            var reader = _readerRepository.GetReaderById(readerId);
            var book = _bookRepository.GetBookById(bookId);

            if (reader == null || book == null)
            {
                Console.WriteLine("Reader or book not found");
                return false;
            }

            if (!reader.CanBorrowBooks)
            {
                Console.WriteLine($"Reader {reader.Name} has reached maximum borrowed books limit");
                return false;
            }

            if (book.Quantity <= 0)
            {
                Console.WriteLine($"Book {book.Title} is not available");
                return false;
            }

            book.Quantity--;
            reader.BorrowedBooks.Add(book);
            
            _bookRepository.UpdateBook(book);
            _readerRepository.UpdateReader(reader);
            
            Console.WriteLine($"Book {book.Title} lent to reader {reader.Name}");
            return true;
        }

        public bool ReturnBook(int readerId, int bookId)
        {
            var reader = _readerRepository.GetReaderById(readerId);
            var book = _bookRepository.GetBookById(bookId);

            if (reader == null || book == null)
            {
                Console.WriteLine("Reader or book not found");
                return false;
            }

            var borrowedBook = reader.BorrowedBooks.FirstOrDefault(b => b.Id == bookId);
            if (borrowedBook == null)
            {
                Console.WriteLine($"Book {book.Title} was not borrowed by reader {reader.Name}");
                return false;
            }

            book.Quantity++;
            reader.BorrowedBooks.Remove(borrowedBook);
            
            _bookRepository.UpdateBook(book);
            _readerRepository.UpdateReader(reader);
            
            Console.WriteLine($"Book {book.Title} returned by reader {reader.Name}");
            return true;
        }

        public bool ReserveBook(int readerId, int bookId)
        {
            var book = _bookRepository.GetBookById(bookId);
            var reader = _readerRepository.GetReaderById(readerId);
            
            if (book == null || reader == null || book.Quantity <= 0)
            {
                Console.WriteLine("Cannot reserve book - invalid conditions");
                return false;
            }

            Console.WriteLine($"Book {book.Title} reserved for reader {reader.Name}");
            return true;
        }

        public void CancelReservation(int readerId, int bookId)
        {
            Console.WriteLine($"Reservation cancelled for book {bookId} and reader {readerId}");
        }
    }
} 