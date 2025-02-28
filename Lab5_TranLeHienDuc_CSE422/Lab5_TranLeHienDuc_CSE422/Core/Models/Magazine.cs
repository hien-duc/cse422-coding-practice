using System;

namespace LibraryManagementSystem.Core.Models
{
    public class Magazine : Document
    {
        public int Issue { get; private set; }

        public Magazine(string title, string publisher, int year, int issue = 1) 
            : base(title, publisher, year)
        {
            Issue = issue;
        }
    }
}