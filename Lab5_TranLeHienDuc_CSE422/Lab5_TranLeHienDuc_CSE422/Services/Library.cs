using System;
using System.Collections.Generic;
using LibraryManagementSystem.Core.Interfaces;
using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.Infrastructure.Data;

namespace LibraryManagementSystem.Services
{
    public class Library : ISubject
    {
        private readonly List<IObserver> _observers = new List<IObserver>();
        private readonly List<Document> _documents = new List<Document>();
        private readonly DatabaseConnection _dbConnection;

        public Library()
        {
            _dbConnection = DatabaseConnection.Instance;
        }

        public void RegisterObserver(IObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
                Console.WriteLine($"User registered for notifications.");
            }
        }

        public void RemoveObserver(IObserver observer)
        {
            if (_observers.Contains(observer))
            {
                _observers.Remove(observer);
                Console.WriteLine($"User unregistered from notifications.");
            }
        }

        public void NotifyObservers(string message)
        {
            foreach (var observer in _observers)
            {
                observer.Update(message);
            }
        }

        public void AddDocument(Document document)
        {
            _documents.Add(document);
            _dbConnection.ExecuteQuery($"INSERT INTO Documents (Title, Publisher, Year) VALUES ('{document.Title}', '{document.Publisher}', {document.Year})");
            NotifyObservers($"New document added: {document}");
        }

        public void BorrowDocument(Document document, User user)
        {
            if (document.IsAvailable)
            {
                document.IsAvailable = false;
                _dbConnection.ExecuteQuery($"INSERT INTO Loans (DocumentId, UserId, LoanDate) VALUES ({_documents.IndexOf(document)}, {user.Id}, '{DateTime.Now}')");
                NotifyObservers($"Document borrowed: {document}");
            }
            else
            {
                Console.WriteLine($"Document is not available: {document}");
            }
        }

        public void ReturnDocument(Document document)
        {
            if (!document.IsAvailable)
            {
                document.IsAvailable = true;
                _dbConnection.ExecuteQuery($"UPDATE Loans SET ReturnDate = '{DateTime.Now}' WHERE DocumentId = {_documents.IndexOf(document)} AND ReturnDate IS NULL");
                NotifyObservers($"Document returned: {document}");
            }
            else
            {
                Console.WriteLine($"Document was not borrowed: {document}");
            }
        }
    }
}