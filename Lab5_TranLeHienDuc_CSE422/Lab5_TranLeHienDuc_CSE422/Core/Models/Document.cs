using System;

namespace LibraryManagementSystem.Core.Models
{
    public abstract class Document
    {
        public string Title { get; protected set; }
        public string Publisher { get; protected set; }
        public int Year { get; protected set; }
        public bool IsAvailable { get; set; } = true;

        public Document(string title, string publisher, int year)
        {
            Title = title;
            Publisher = publisher;
            Year = year;
        }

        public override string ToString()
        {
            return $"{GetType().Name}: '{Title}' by {Publisher} ({Year})";
        }
    }
}