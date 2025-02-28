using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public class BookRepository : IBookRepository
    {
        private List<IBook> _books;

        public BookRepository()
        {
            _books = new List<IBook>();
        }

        public IBook GetBookById(int id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }

        public IEnumerable<IBook> GetBooksByTitle(string title)
        {
            return _books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<IBook> GetBooksByCategory(string category)
        {
            if (string.IsNullOrEmpty(category))
                return _books;
            return _books.Where(b => b.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
        }

        public void AddBook(IBook book)
        {
            _books.Add(book);
        }

        public void UpdateBook(IBook book)
        {
            var existingBook = GetBookById(book.Id);
            if (existingBook != null)
            {
                var index = _books.IndexOf(existingBook);
                _books[index] = book;
            }
        }

        public bool DeleteBook(int id)
        {
            var book = GetBookById(id);
            if (book != null)
            {
                return _books.Remove(book);
            }
            return false;
        }

        public void InitializeTestData()
        {
            _books = new List<IBook>
            {
                new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Category = "Fiction", Quantity = 5 },
                new Book { Id = 2, Title = "1984", Author = "George Orwell", Category = "Fiction", Quantity = 3 },
                new Book { Id = 3, Title = "Clean Code", Author = "Robert C. Martin", Category = "Programming", Quantity = 2 },
                new Book { Id = 4, Title = "Design Patterns", Author = "Gang of Four", Category = "Programming", Quantity = 1 },
                new Book { Id = 5, Title = "The Hobbit", Author = "J.R.R. Tolkien", Category = "Fantasy", Quantity = 4 }
            };
        }
    }
} 