using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_TranLeHienDuc_CSE422
{
    class TransactionHandler
    {
        private List<Transaction> _transactions;

        public TransactionHandler()
        {
            _transactions = new List<Transaction>();
        }

        public void AddTransaction(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));
            _transactions.Add(transaction);
        }

        public void ExecuteAllTransactions()
        {
            foreach (var transaction in _transactions)
            {
                try
                {
                    Console.WriteLine($"\nExecuting transaction {transaction.TransactionID}:");
                    Console.WriteLine($"Type: {transaction.GetType().Name}");
                    Console.WriteLine($"Date: {transaction.TransactionDate}");
                    Console.WriteLine($"Member: {transaction.Member.Name}");

                    // Using polymorphism to call the appropriate Execute method
                    transaction.Execute();

                    // Display specific details based on transaction type
                    if (transaction is BorrowTransaction borrowTrans)
                    {
                        Console.WriteLine($"Book borrowed: {borrowTrans.BookBorrowed.Title}");
                    }
                    else if (transaction is ReturnTransaction returnTrans)
                    {
                        Console.WriteLine($"Book returned: {returnTrans.BookReturned.Title}");
                    }

                    Console.WriteLine("Transaction executed successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error executing transaction {transaction.TransactionID}: {ex.Message}");
                }
            }
        }

        public void DisplayAllTransactions()
        {
            Console.WriteLine("\nAll Transactions:");
            foreach (var transaction in _transactions)
            {
                Console.WriteLine($"ID: {transaction.TransactionID}, Type: {transaction.GetType().Name}, Date: {transaction.TransactionDate}");
            }
        }
    }
}
