using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup repositories with test data
            var bookRepository = new BookRepository();
            var readerRepository = new ReaderRepository();
            InitializeTestData(bookRepository, readerRepository);

            // Setup services
            var libraryService = new LibraryService(bookRepository, readerRepository);
            var reportService = new ReportService(bookRepository, readerRepository);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Library Management System ===\n");
                Console.WriteLine("1. Display All Books");
                Console.WriteLine("2. Search Books by Category");
                Console.WriteLine("3. Search Books by Title");
                Console.WriteLine("4. Add New Book");
                Console.WriteLine("5. Lend Book");
                Console.WriteLine("6. Return Book");
                Console.WriteLine("7. View Reader Report");
                Console.WriteLine("8. View All Borrowed Books");
                Console.WriteLine("9. View Book Inventory");
                Console.WriteLine("0. Exit");

                Console.Write("\nSelect an option: ");
                var choice = Console.ReadLine();

                Console.Clear();
                switch (choice)
                {
                    case "1":
                        DisplayAllBooks(bookRepository);
                        break;
                    case "2":
                        SearchBooksByCategory(libraryService);
                        break;
                    case "3":
                        SearchBooksByTitle(libraryService);
                        break;
                    case "4":
                        AddNewBook(libraryService);
                        break;
                    case "5":
                        LendBook(libraryService);
                        break;
                    case "6":
                        ReturnBook(libraryService);
                        break;
                    case "7":
                        ViewReaderReport(reportService);
                        break;
                    case "8":
                        ViewBorrowedBooksReport(reportService);
                        break;
                    case "9":
                        ViewBookInventory(reportService);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option!");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        private static void InitializeTestData(IBookRepository bookRepository, IReaderRepository readerRepository)
        {
            // Add sample books
            var books = new List<IBook>
            {
                new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Category = "Fiction", Quantity = 3 },
                new Book { Id = 2, Title = "1984", Author = "George Orwell", Category = "Fiction", Quantity = 2 },
                new Book { Id = 3, Title = "Clean Code", Author = "Robert C. Martin", Category = "Programming", Quantity = 4 },
                new Book { Id = 4, Title = "Design Patterns", Author = "Gang of Four", Category = "Programming", Quantity = 2 },
                new Book { Id = 5, Title = "The Hobbit", Author = "J.R.R. Tolkien", Category = "Fantasy", Quantity = 3 }
            };

            foreach (var book in books)
            {
                bookRepository.AddBook(book);
            }

            // Add sample readers
            var readers = new List<IReader>
            {
                new Reader { Id = 1, Name = "John Doe" },
                new Reader { Id = 2, Name = "Jane Smith" },
                new Reader { Id = 3, Name = "Bob Johnson" }
            };

            foreach (var reader in readers)
            {
                readerRepository.AddReader(reader);
            }
        }

        private static void DisplayAllBooks(IBookRepository bookRepository)
        {
            Console.WriteLine("=== All Books ===\n");
            var books = bookRepository.GetBooksByCategory("");
            foreach (var book in books)
            {
                Console.WriteLine($"ID: {book.Id} - {book.Title} by {book.Author}");
                Console.WriteLine($"Category: {book.Category}");
                Console.WriteLine($"Available Copies: {book.Quantity}\n");
            }
        }

        private static void SearchBooksByCategory(ILibraryService libraryService)
        {
            Console.Write("Enter category to search: ");
            var category = Console.ReadLine();
            
            var books = libraryService.SearchBooksByCategory(category);
            Console.WriteLine($"\nBooks in category '{category}':");
            foreach (var book in books)
            {
                Console.WriteLine($"- {book.Title} by {book.Author} ({book.Quantity} copies available)");
            }
        }

        private static void SearchBooksByTitle(ILibraryService libraryService)
        {
            Console.Write("Enter title to search: ");
            var title = Console.ReadLine();
            
            var books = libraryService.SearchBooksByTitle(title);
            Console.WriteLine($"\nBooks matching '{title}':");
            foreach (var book in books)
            {
                Console.WriteLine($"- {book.Title} by {book.Author} ({book.Quantity} copies available)");
            }
        }

        private static void AddNewBook(ILibraryService libraryService)
        {
            Console.WriteLine("=== Add New Book ===\n");
            
            Console.Write("Enter Title: ");
            var title = Console.ReadLine();
            
            Console.Write("Enter Author: ");
            var author = Console.ReadLine();
            
            Console.Write("Enter Category: ");
            var category = Console.ReadLine();
            
            Console.Write("Enter Quantity: ");
            if (int.TryParse(Console.ReadLine(), out int quantity))
            {
                var book = new Book
                {
                    Id = new Random().Next(100, 999), // Simple ID generation
                    Title = title,
                    Author = author,
                    Category = category,
                    Quantity = quantity
                };
                
                libraryService.AddBook(book);
                Console.WriteLine("\nBook added successfully!");
            }
            else
            {
                Console.WriteLine("\nInvalid quantity!");
            }
        }

        private static void LendBook(ILibraryService libraryService)
        {
            Console.Write("Enter Reader ID: ");
            if (int.TryParse(Console.ReadLine(), out int readerId))
            {
                Console.Write("Enter Book ID: ");
                if (int.TryParse(Console.ReadLine(), out int bookId))
                {
                    var success = libraryService.LendBook(readerId, bookId);
                    if (success)
                        Console.WriteLine("\nBook lent successfully!");
                    else
                        Console.WriteLine("\nFailed to lend book. Please check reader ID, book availability, and borrowing limits.");
                }
            }
        }

        private static void ReturnBook(ILibraryService libraryService)
        {
            Console.Write("Enter Reader ID: ");
            if (int.TryParse(Console.ReadLine(), out int readerId))
            {
                Console.Write("Enter Book ID: ");
                if (int.TryParse(Console.ReadLine(), out int bookId))
                {
                    var success = libraryService.ReturnBook(readerId, bookId);
                    if (success)
                        Console.WriteLine("\nBook returned successfully!");
                    else
                        Console.WriteLine("\nFailed to return book. Please check reader ID and book ID.");
                }
            }
        }

        private static void ViewReaderReport(IReportService reportService)
        {
            Console.Write("Enter Reader ID: ");
            if (int.TryParse(Console.ReadLine(), out int readerId))
            {
                var report = reportService.GenerateReaderReport(readerId);
                Console.WriteLine("\n" + report);
            }
        }

        private static void ViewBorrowedBooksReport(IReportService reportService)
        {
            var report = reportService.GenerateBorrowedBooksReport();
            Console.WriteLine(report);
        }

        private static void ViewBookInventory(IReportService reportService)
        {
            var report = reportService.GenerateBookInventoryReport();
            Console.WriteLine(report);
        }
    }
}
