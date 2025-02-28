using System;

namespace LibraryManagementSystem.Core.Models
{
    public class Newspaper : Document
    {
        public DateTime PublicationDate { get; private set; }

        public Newspaper(string title, string publisher, int year) 
            : base(title, publisher, year)
        {
            PublicationDate = DateTime.Now;
        }
    }
}