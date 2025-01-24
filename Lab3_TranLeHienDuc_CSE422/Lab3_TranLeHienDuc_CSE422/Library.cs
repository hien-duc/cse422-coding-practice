using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3_TranLeHienDuc_CSE422
{
    public class Library
    {
        private readonly string _libraryName;
        private readonly List<Book> _books;
        private readonly List<Member> _members;

        public event Action<Book, Member> OnBookBorrowed;
        public event Action<Book, Member> OnBookReturned;
        public event Action<Member> OnNewMemberAdded;

        public Library(string libraryName)
        {
            _libraryName = libraryName;
            _books = new List<Book>();
            _members = new List<Member>();
        }

        public Library(string libraryName, List<Book> initialBooks)
        {
            _libraryName = libraryName;
            _books = new List<Book>(initialBooks ?? throw new ArgumentNullException(nameof(initialBooks)));
            _members = new List<Member>();
        }

        public Library(Library other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            _libraryName = other._libraryName;
            _books = new List<Book>(other._books);
            _members = new List<Member>(other._members);
        }

        public void AddBook(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            _books.Add(book);
        }

        public void AddMember(Member member)
        {
            if (member == null) throw new ArgumentNullException(nameof(member));
            _members.Add(member);
            OnNewMemberAdded?.Invoke(member);
        }

        public void BorrowBook(Book book, Member member)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            if (member == null) throw new ArgumentNullException(nameof(member));

            if (!_books.Contains(book))
                throw new InvalidOperationException("Book is not available in the library");
            if (!_members.Contains(member))
                throw new InvalidOperationException("Member is not registered with the library");

            member.BorrowBook(book);
            OnBookBorrowed?.Invoke(book, member);
        }

        public void ReturnBook(Book book, Member member)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            if (member == null) throw new ArgumentNullException(nameof(member));

            if (!_members.Contains(member))
                throw new InvalidOperationException("Member is not registered with the library");

            member.ReturnBook(book);
            OnBookReturned?.Invoke(book, member);
        }

        public void DisplayLibraryInfo()
        {
            Console.WriteLine($"\nLibrary: {_libraryName}");
            Console.WriteLine($"Total Books: {_books.Count}");
            Console.WriteLine($"Total Members: {_members.Count}");

            Console.WriteLine("\nBooks in Library:");
            foreach (var book in _books)
            {
                Console.WriteLine($"- {book.Title} by {book.Author} ({book.CopiesAvailable} copies available)");
            }

            Console.WriteLine("\nRegistered Members:");
            foreach (var member in _members)
            {
                Console.WriteLine($"- {member.Name} ({(member is PremiumMember ? "Premium" : "Regular")})");
            }
        }

        public List<Book> GetAvailableBooks()
        {
            return _books.Where(b => b.IsAvailable).ToList();
        }

        public Member GetMemberById(string memberId)
        {
            return _members.FirstOrDefault(m => m.MemberID == memberId);
        }

        public Book GetBookByISBN(string isbn)
        {
            return _books.FirstOrDefault(b => b.ISBN == isbn);
        }
    }
}
