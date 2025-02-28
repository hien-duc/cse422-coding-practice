using System;
using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.Core.Interfaces;
using LibraryManagementSystem.Infrastructure.Data;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Library Management System Demo");
                Console.WriteLine("================================\n");

                DemonstrateSingletonPattern();
                var documents = DemonstrateFactoryPattern();
                var library = DemonstrateObserverPattern(documents.book, documents.magazine);
                DemonstrateStrategyPattern(documents.book, documents.magazine, documents.newspaper);

                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }

        static void DemonstrateSingletonPattern()
        {
            Console.WriteLine("Demonstrating Singleton Pattern for Database Connection:");
            var dbConnection1 = DatabaseConnection.Instance;
            var dbConnection2 = DatabaseConnection.Instance;
            Console.WriteLine($"Are both database connections the same instance? {ReferenceEquals(dbConnection1, dbConnection2)}\n");
        }

        static (Document book, Document magazine, Document newspaper) DemonstrateFactoryPattern()
        {
            Console.WriteLine("Demonstrating Factory Method Pattern for Document Creation:");
            var documentFactory = new DocumentFactory();
            
            var book = documentFactory.CreateDocument("Book", "Clean Code", "Robert C. Martin", 2008);
            var magazine = documentFactory.CreateDocument("Magazine", "National Geographic", "National Geographic Society", 2023);
            var newspaper = documentFactory.CreateDocument("Newspaper", "The New York Times", "The New York Times Company", 2023);
            
            Console.WriteLine($"Created: {book}");
            Console.WriteLine($"Created: {magazine}");
            Console.WriteLine($"Created: {newspaper}\n");

            return (book, magazine, newspaper);
        }

        static Library DemonstrateObserverPattern(Document book, Document magazine)
        {
            Console.WriteLine("Demonstrating Observer Pattern for Notifications:");
            var library = new Library();
            var user1 = new User(1, "John Doe", "john@example.com");
            var user2 = new User(2, "Jane Smith", "jane@example.com");
            
            library.RegisterObserver(user1);
            library.RegisterObserver(user2);
            
            Console.WriteLine("Adding a new book to the library...");
            library.AddDocument(book);
            
            Console.WriteLine("\nUser 1 borrows the book...");
            library.BorrowDocument(book, user1);
            
            Console.WriteLine("\nUser 1 returns the book...");
            library.ReturnDocument(book);
            
            Console.WriteLine("\nRemoving User 2 from notifications...");
            library.RemoveObserver(user2);
            
            Console.WriteLine("\nAdding a new magazine to the library...");
            library.AddDocument(magazine);

            return library;
        }

        static void DemonstrateStrategyPattern(Document book, Document magazine, Document newspaper)
        {
            Console.WriteLine("\nDemonstrating Strategy Pattern for Loan Fee Calculation:");
            var loanProcessor = new LoanProcessor();
            
            int daysBook = 14;
            int daysMagazine = 7;
            int daysNewspaper = 2;
            
            decimal bookFee = loanProcessor.CalculateFee(book, daysBook);
            decimal magazineFee = loanProcessor.CalculateFee(magazine, daysMagazine);
            decimal newspaperFee = loanProcessor.CalculateFee(newspaper, daysNewspaper);
            
            Console.WriteLine($"Loan fee for '{book.Title}' for {daysBook} days: ${bookFee:F2}");
            Console.WriteLine($"Loan fee for '{magazine.Title}' for {daysMagazine} days: ${magazineFee:F2}");
            Console.WriteLine($"Loan fee for '{newspaper.Title}' for {daysNewspaper} days: ${newspaperFee:F2}");
        }
    }
}
