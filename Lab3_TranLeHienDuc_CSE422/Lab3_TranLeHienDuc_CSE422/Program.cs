using System;
using System.Collections.Generic;

namespace Lab3_TranLeHienDuc_CSE422
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Library Management System Demo ===\n");

            // Exercise 1: Book Class Demo
            Console.WriteLine("1. Book Class Demonstration:");
            var book1 = new Book("978-0743273565", "The Great Gatsby", "F. Scott Fitzgerald", 1925, 2);
            var book2 = new Book("978-0446310789", "To Kill a Mockingbird", "Harper Lee", 1960, 1);
            var book3 = new Book("978-0141439518", "Pride and Prejudice", "Jane Austen", 1813, 3);
            book1.DisplayInfo();

            // Exercise 2: Member and PremiumMember Demo
            Console.WriteLine("\n2. Member Classes Demonstration:");
            var member = new Member("M001", "John Doe", "john@example.com");
            var premiumMember = new PremiumMember("P001", "Jane Smith", "jane@example.com", 
                DateTime.Now.AddYears(1), 5);
            member.DisplayInfo();
            premiumMember.DisplayInfo();

            // Exercise 3 & 4: Transactions Demo
            Console.WriteLine("\n3 + 4. Transactions Demonstration:");
            var transactionHandler = new TransactionHandler();

            var borrowTransaction = new BorrowTransaction
            {
                TransactionID = "BRW001",
                TransactionDate = DateTime.Now,
                Member = member,
                BookBorrowed = book1
            };

            var returnTransaction = new ReturnTransaction
            {
                TransactionID = "RTN001",
                TransactionDate = DateTime.Now,
                Member = premiumMember,
                BookReturned = book2
            };

            transactionHandler.AddTransaction(borrowTransaction);
            transactionHandler.AddTransaction(returnTransaction);
            transactionHandler.ExecuteAllTransactions();
            transactionHandler.DisplayAllTransactions();

            // Exercise 5: Interfaces Demo
            Console.WriteLine("\n5. Interface Implementation Demonstration:");
            IPrintable printableBook = book3;
            IPrintable printableMember = member;
            printableBook.PrintDetails();
            printableMember.PrintDetails();

            // Exercise 6: Library Class Demo
            Console.WriteLine("\n6. Library Class Demonstration:");
            var library = new Library("Central Library", new List<Book> { book1, book2, book3 });
            library.AddMember(member);
            library.AddMember(premiumMember);
            library.DisplayLibraryInfo();

            // Exercise 7: Notification Service Demo
            Console.WriteLine("\n7. Notification Service Demonstration:");
            var notificationService = new NotificationService(library, includeTimestamp: true);
            var advancedNotificationService = new AdvancedNotificationService(library);

            notificationService.SendNotification("Regular notification");
            notificationService.SendNotification("Personal message", "John");
            notificationService.SendNotification("Group message", new List<string> { "John", "Jane" });
            advancedNotificationService.SendNotification("Advanced notification with timestamp");

            // Exercise 8: Library Card Demo
            Console.WriteLine("\n8. Library Card Demonstration:");
            var card = new LibraryCard("LC001", member);
            card.DisplayCardInfo();
            card.RenewCard();
            card.DisplayCardInfo();

            // Exercise 9: BookClass vs BookRecord Demo
            Console.WriteLine("\n9. BookClass vs BookRecord Demonstration:");
            var bookClass1 = new BookClass("123", "Book Title", "Author Name");
            var bookClass2 = new BookClass("123", "Book Title", "Author Name");
            var bookRecord1 = new BookRecord("123", "Book Title", "Author Name");
            var bookRecord2 = new BookRecord("123", "Book Title", "Author Name");

            Console.WriteLine($"BookClass comparison (==): {bookClass1 == bookClass2}");
            Console.WriteLine($"BookRecord comparison (==): {bookRecord1 == bookRecord2}");

            var modifiedRecord = bookRecord1 with { Title = "New Title" };
            Console.WriteLine($"Modified record: {modifiedRecord}");

            // Exercise 10: Events Demo
            Console.WriteLine("\n10. Events Demonstration:");
            try
            {
                Console.WriteLine("Demonstrating book borrowing event:");
                library.BorrowBook(book3, member);
                Console.WriteLine("\nDemonstrating book returning event:");
                library.ReturnBook(book3, member);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Operation failed: {ex.Message}");
            }

            // Clean up
            notificationService.Unsubscribe();
            advancedNotificationService.Unsubscribe();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
