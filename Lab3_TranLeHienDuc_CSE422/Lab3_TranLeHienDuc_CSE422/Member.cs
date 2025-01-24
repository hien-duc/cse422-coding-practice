using System;
using System.Collections.Generic;

namespace Lab3_TranLeHienDuc_CSE422
{
    public class Member : IPrintable, IMemberActions
    {
        private string _memberID;
        private string _name;
        private string _email;
        private List<Book> _borrowedBooks;

        public string MemberID 
        { 
            get => _memberID;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Member ID cannot be empty or null");
                _memberID = value;
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty or null");
                _name = value;
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Email cannot be empty or null");
                if (!value.Contains("@"))
                    throw new ArgumentException("Invalid email format");
                _email = value;
            }
        }

        public List<Book> BorrowedBooks
        {
            get => _borrowedBooks;
            protected set => _borrowedBooks = value ?? new List<Book>();
        }

        public Member(string memberID, string name, string email)
        {
            MemberID = memberID;
            Name = name;
            Email = email;
            BorrowedBooks = new List<Book>();
        }

        public virtual void BorrowBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));
            if (!book.IsAvailable)
                throw new InvalidOperationException("Book is not available for borrowing");
            if (BorrowedBooks.Contains(book))
                throw new InvalidOperationException("You have already borrowed this book");

            book.CopiesAvailable--;
            BorrowedBooks.Add(book);
        }

        public virtual void ReturnBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));
            if (!BorrowedBooks.Contains(book))
                throw new InvalidOperationException("You haven't borrowed this book");

            book.CopiesAvailable++;
            BorrowedBooks.Remove(book);
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Member Information:");
            Console.WriteLine($"ID: {MemberID}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Currently Borrowed Books: {BorrowedBooks.Count}");
            
            if (BorrowedBooks.Count > 0)
            {
                Console.WriteLine("Borrowed Books:");
                foreach (var book in BorrowedBooks)
                {
                    Console.WriteLine($"- {book.Title} by {book.Author}");
                }
            }
        }

        public void PrintDetails()
        {
            DisplayInfo();
        }
    }
}