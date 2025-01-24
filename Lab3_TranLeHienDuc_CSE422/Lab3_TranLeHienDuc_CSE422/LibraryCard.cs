using System;

namespace Lab3_TranLeHienDuc_CSE422
{
    public class LibraryCard
    {
        private readonly string _cardNumber;
        private Member _owner;
        private DateTime _issueDate;

        public string CardNumber => _cardNumber;

        public Member Owner
        {
            get => _owner;
            set => _owner = value ?? throw new ArgumentNullException(nameof(value));
        }

        public DateTime IssueDate => _issueDate;

        public LibraryCard(string cardNumber, Member owner)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                throw new ArgumentException("Card number cannot be empty or null");

            _cardNumber = cardNumber;
            Owner = owner;
            _issueDate = DateTime.Now;
        }

        public void RenewCard()
        {
            if (_owner is PremiumMember premiumMember && !premiumMember.IsMembershipValid)
            {
                throw new InvalidOperationException("Cannot renew card for expired premium membership");
            }

            _issueDate = DateTime.Now;
        }

        public void DisplayCardInfo()
        {
            Console.WriteLine("Library Card Information:");
            Console.WriteLine($"Card Number: {CardNumber}");
            Console.WriteLine($"Owner: {Owner.Name}");
            Console.WriteLine($"Issue Date: {IssueDate:d}");
            Console.WriteLine($"Member Type: {(Owner is PremiumMember ? "Premium" : "Regular")}");
        }
    }
}
