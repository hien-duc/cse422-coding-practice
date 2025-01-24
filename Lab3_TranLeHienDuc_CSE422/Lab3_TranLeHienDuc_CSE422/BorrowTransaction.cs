using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_TranLeHienDuc_CSE422
{
    public class BorrowTransaction : Transaction
    {
        private Book _bookBorrowed;

        public Book BookBorrowed
        {
            get => _bookBorrowed;
            set => _bookBorrowed = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override void Execute()
        {
            if (BookBorrowed.CopiesAvailable <= 0)
            {
                throw new InvalidOperationException("Book is not available for borrowing");
            }

            if (Member.BorrowedBooks.Contains(BookBorrowed))
            {
                throw new InvalidOperationException("Member has already borrowed this book");
            }

            BookBorrowed.CopiesAvailable--;
            Member.BorrowedBooks.Add(BookBorrowed);
        }

        public override string ToString()
        {
            return $"Borrow Transaction - Book: {BookBorrowed.Title}, Member: {Member.Name}, Date: {TransactionDate:d}";
        }
    }
}
