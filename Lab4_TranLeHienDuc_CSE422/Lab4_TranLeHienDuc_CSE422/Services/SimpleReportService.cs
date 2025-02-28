
using System.Text;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Services
{
    public class SimpleReportService : IReportService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IReaderRepository _readerRepository;

        public SimpleReportService(IBookRepository bookRepository, IReaderRepository readerRepository)
        {
            _bookRepository = bookRepository;
            _readerRepository = readerRepository;
        }

        public string GenerateReaderReport(int readerId)
        {
            var reader = _readerRepository.GetReaderById(readerId);
            if (reader == null)
                return "Reader not found";

            var report = new StringBuilder();
            report.AppendLine($"Reader: {reader.Name}");
            report.AppendLine($"Books borrowed: {reader.BorrowedBooks.Count}/{reader.MaxBorrowedBooks}");
            
            if (reader.BorrowedBooks.Any())
            {
                report.AppendLine("\nCurrently borrowed books:");
                foreach (var book in reader.BorrowedBooks)
                {
                    report.AppendLine($"- {book.Title} by {book.Author}");
                }
            }
            
            return report.ToString();
        }

        public string GenerateBorrowedBooksReport()
        {
            var report = new StringBuilder();
            report.AppendLine("BORROWED BOOKS REPORT");
            var readers = _readerRepository.GetAllReaders();
            
            foreach (var reader in readers.Where(r => r.BorrowedBooks.Any()))
            {
                report.AppendLine($"\nReader: {reader.Name}");
                foreach (var book in reader.BorrowedBooks)
                {
                    report.AppendLine($"- {book.Title}");
                }
            }
            
            return report.ToString();
        }

        public string GenerateBookInventoryReport()
        {
            var report = new StringBuilder();
            report.AppendLine("BOOK INVENTORY");
            var books = _bookRepository.GetBooksByCategory("");
            
            foreach (var book in books)
            {
                report.AppendLine($"\n{book.Title} by {book.Author}");
                report.AppendLine($"Category: {book.Category}");
                report.AppendLine($"Copies available: {book.Quantity}");
            }
            
            return report.ToString();
        }
    }
} 