using System;
using System.Linq;
using System.Text;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Services
{
    public class ReportService : IReportService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IReaderRepository _readerRepository;

        public ReportService(IBookRepository bookRepository, IReaderRepository readerRepository)
        {
            _bookRepository = bookRepository;
            _readerRepository = readerRepository;
        }

        public string GenerateBorrowedBooksReport()
        {
            var readers = _readerRepository.GetAllReaders();
            var report = new StringBuilder();
            
            report.AppendLine("BORROWED BOOKS REPORT");
            report.AppendLine("=====================");
            
            foreach (var reader in readers)
            {
                if (reader.BorrowedBooks.Any())
                {
                    report.AppendLine($"\nReader: {reader.Name} (ID: {reader.Id})");
                    foreach (var book in reader.BorrowedBooks)
                    {
                        report.AppendLine($"- {book.Title} by {book.Author}");
                    }
                }
            }
            
            return report.ToString();
        }

        public string GenerateReaderReport(int readerId)
        {
            var reader = _readerRepository.GetReaderById(readerId);
            if (reader == null)
            {
                return "Reader not found";
            }

            var report = new StringBuilder();
            report.AppendLine($"READER REPORT - {reader.Name}");
            report.AppendLine("=====================");
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

        public string GenerateBookInventoryReport()
        {
            var books = _bookRepository.GetBooksByCategory("");  // Get all books
            var report = new StringBuilder();
            
            report.AppendLine("BOOK INVENTORY REPORT");
            report.AppendLine("====================");
            
            foreach (var book in books)
            {
                report.AppendLine($"\nTitle: {book.Title}");
                report.AppendLine($"Author: {book.Author}");
                report.AppendLine($"Category: {book.Category}");
                report.AppendLine($"Type: {book.BookType}");
                report.AppendLine($"Quantity: {book.Quantity}");
            }
            
            return report.ToString();
        }
    }
} 