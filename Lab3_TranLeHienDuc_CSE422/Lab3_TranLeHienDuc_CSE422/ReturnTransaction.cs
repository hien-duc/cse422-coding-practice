using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_TranLeHienDuc_CSE422
{
    public class ReturnTransaction : Transaction
    {
        private Book _bookReturned;

        public Book BookReturned
        {
            get => _bookReturned;
            set => _bookReturned = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override void Execute()
        {
            if (!Member.BorrowedBooks.Contains(BookReturned))
            {
                throw new InvalidOperationException("This book was not borrowed by this member");
            }

            BookReturned.CopiesAvailable++;
            Member.BorrowedBooks.Remove(BookReturned);
        }

        public override string ToString()
        {
            return $"Return Transaction - Book: {BookReturned.Title}, Member: {Member.Name}, Date: {TransactionDate:d}";
        }
    }
}
