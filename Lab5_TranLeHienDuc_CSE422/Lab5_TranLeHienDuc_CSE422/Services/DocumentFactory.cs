using System;
using LibraryManagementSystem.Core.Models;

namespace LibraryManagementSystem.Services
{
    public class DocumentFactory
    {
        public Document CreateDocument(string type, string title, string publisher, int year)
        {
            return type.ToLower() switch
            {
                "book" => new Book(title, publisher, year),
                "magazine" => new Magazine(title, publisher, year),
                "newspaper" => new Newspaper(title, publisher, year),
                _ => throw new ArgumentException($"Invalid document type: {type}")
            };
        }
    }
}