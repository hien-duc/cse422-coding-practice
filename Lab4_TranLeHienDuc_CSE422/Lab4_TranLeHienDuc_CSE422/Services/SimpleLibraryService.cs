
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Services
{
    public class SimpleLibraryService : ILibraryService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IReaderRepository _readerRepository;

        public SimpleLibraryService(IBookRepository bookRepository, IReaderRepository readerRepository)
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

        public bool LendBook(int readerId, int bookId)
        {
            var reader = _readerRepository.GetReaderById(readerId);
            var book = _bookRepository.GetBookById(bookId);

            if (reader == null || book == null || !reader.CanBorrowBooks || book.Quantity <= 0)
                return false;

            book.Quantity--;
            reader.BorrowedBooks.Add(book);
            
            _bookRepository.UpdateBook(book);
            _readerRepository.UpdateReader(reader);
            
            return true;
        }

        public bool ReturnBook(int readerId, int bookId)
        {
            var reader = _readerRepository.GetReaderById(readerId);
            var book = _bookRepository.GetBookById(bookId);

            if (reader == null || book == null)
                return false;

            var borrowedBook = reader.BorrowedBooks.FirstOrDefault(b => b.Id == bookId);
            if (borrowedBook == null)
                return false;

            book.Quantity++;
            reader.BorrowedBooks.Remove(borrowedBook);
            
            _bookRepository.UpdateBook(book);
            _readerRepository.UpdateReader(reader);
            
            return true;
        }

        public bool ReserveBook(int readerId, int bookId)
        {
            var reader = _readerRepository.GetReaderById(readerId);
            var book = _bookRepository.GetBookById(bookId);
    
            if (reader == null || book == null || book.Quantity <= 0)
                return false;
    
            return true;
        }

        public void CancelReservation(int readerId, int bookId)
        {
            var reader = _readerRepository.GetReaderById(readerId);
            var book = _bookRepository.GetBookById(bookId);
    
            if (reader == null || book == null)
                return;
    
            // If the book is in the reader's borrowed list, remove it
            var reservedBook = reader.BorrowedBooks.FirstOrDefault(b => b.Id == bookId);
            if (reservedBook != null)
            {
                book.Quantity++;
                reader.BorrowedBooks.Remove(reservedBook);
                
                _bookRepository.UpdateBook(book);
                _readerRepository.UpdateReader(reader);
            }
        }

        public void AddReader(IReader reader)
        {
            _readerRepository.AddReader(reader);
        }

        public IReader GetReaderById(int id)
        {
            return _readerRepository.GetReaderById(id);
        }
    }
}