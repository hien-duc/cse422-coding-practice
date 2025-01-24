using System;
using System.Collections.Generic;

namespace Lab3_TranLeHienDuc_CSE422
{
    public class NotificationService
    {
        private readonly Library _library;
        private readonly bool _includeTimestamp;

        public NotificationService(Library library, bool includeTimestamp = false)
        {
            _library = library ?? throw new ArgumentNullException(nameof(library));
            _includeTimestamp = includeTimestamp;
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _library.OnBookBorrowed += HandleBookBorrowed;
            _library.OnBookReturned += HandleBookReturned;
            _library.OnNewMemberAdded += HandleNewMember;
        }

        public void Unsubscribe()
        {
            _library.OnBookBorrowed -= HandleBookBorrowed;
            _library.OnBookReturned -= HandleBookReturned;
            _library.OnNewMemberAdded -= HandleNewMember;
        }

        private void HandleBookBorrowed(Book book, Member member)
        {
            SendNotification($"Book '{book.Title}' has been borrowed by {member.Name}");
        }

        private void HandleBookReturned(Book book, Member member)
        {
            SendNotification($"Book '{book.Title}' has been returned by {member.Name}");
        }

        private void HandleNewMember(Member member)
        {
            SendNotification($"New member registered: {member.Name}");
        }

        public virtual void SendNotification(string message)
        {
            var timestamp = _includeTimestamp ? $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] " : "";
            Console.WriteLine($"{timestamp}Notification: {message}");
        }

        public virtual void SendNotification(string message, string recipient)
        {
            var timestamp = _includeTimestamp ? $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] " : "";
            Console.WriteLine($"{timestamp}Notification to {recipient}: {message}");
        }

        public virtual void SendNotification(string message, List<string> recipients)
        {
            var timestamp = _includeTimestamp ? $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] " : "";
            Console.WriteLine($"{timestamp}Group notification to {string.Join(", ", recipients)}: {message}");
        }
    }
}
