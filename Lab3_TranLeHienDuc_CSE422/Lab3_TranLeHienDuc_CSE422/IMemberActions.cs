using System;

namespace Lab3_TranLeHienDuc_CSE422
{
    public interface IMemberActions
    {
        void BorrowBook(Book book);
        void ReturnBook(Book book);
    }
}
