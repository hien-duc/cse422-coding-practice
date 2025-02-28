using System;
using LibraryManagementSystem.Core.Models;

namespace LibraryManagementSystem.Services
{
    public interface ILoanFeeStrategy
    {
        decimal CalculateFee(int days);
    }

    public class BookLoanFeeStrategy : ILoanFeeStrategy
    {
        private const decimal DailyRate = 0.50m;
        private const int GracePeriod = 14; // 14 days grace period for books

        public decimal CalculateFee(int days)
        {
            if (days <= GracePeriod)
                return 0;
            
            return (days - GracePeriod) * DailyRate;
        }
    }

    public class MagazineLoanFeeStrategy : ILoanFeeStrategy
    {
        private const decimal DailyRate = 1.00m;
        private const int GracePeriod = 7; // 7 days grace period for magazines

        public decimal CalculateFee(int days)
        {
            if (days <= GracePeriod)
                return 0;
            
            return (days - GracePeriod) * DailyRate;
        }
    }

    public class NewspaperLoanFeeStrategy : ILoanFeeStrategy
    {
        private const decimal DailyRate = 2.00m;
        private const int GracePeriod = 1; // 1 day grace period for newspapers

        public decimal CalculateFee(int days)
        {
            if (days <= GracePeriod)
                return 0;
            
            return (days - GracePeriod) * DailyRate;
        }
    }

    public class LoanProcessor
    {
        public decimal CalculateFee(Document document, int days)
        {
            ILoanFeeStrategy strategy = document switch
            {
                Book => new BookLoanFeeStrategy(),
                Magazine => new MagazineLoanFeeStrategy(),
                Newspaper => new NewspaperLoanFeeStrategy(),
                _ => throw new ArgumentException($"Unknown document type: {document.GetType().Name}")
            };

            return strategy.CalculateFee(days);
        }
    }
}