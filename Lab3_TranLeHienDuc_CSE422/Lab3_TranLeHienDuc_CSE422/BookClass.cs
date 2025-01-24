using System;

namespace Lab3_TranLeHienDuc_CSE422
{
    public class BookClass
    {
        private string _isbn;
        private string _title;
        private string _author;

        public BookClass(string isbn, string title, string author)
        {
            ISBN = isbn;
            Title = title;
            Author = author;
        }

        public string ISBN
        {
            get => _isbn;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("ISBN cannot be empty or null");
                _isbn = value;
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Title cannot be empty or null");
                _title = value;
            }
        }

        public string Author
        {
            get => _author;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Author cannot be empty or null");
                _author = value;
            }
        }

        public override string ToString()
        {
            return $"{_isbn} by {_author} (ISBN: {_isbn})";
        }
    }
}
