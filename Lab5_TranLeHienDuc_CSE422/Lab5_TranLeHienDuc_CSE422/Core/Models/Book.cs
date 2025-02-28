using System;

namespace LibraryManagementSystem.Core.Models
{
    public class Book : Document
    {
        public string Author { get; private set; }

        public Book(string title, string publisher, int year, string author = "") 
            : base(title, publisher, year)
        {
            Author = author;
        }

        public override string ToString()
        {
            return $"{GetType().Name}: '{Title}' by {(string.IsNullOrEmpty(Author) ? Publisher : Author)} ({Year})";
        }
    }
}