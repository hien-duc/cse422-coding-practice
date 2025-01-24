using System;

namespace Lab3_TranLeHienDuc_CSE422;

public class PremiumMember : Member
{
    private DateTime _membershipExpiry;
    private int _maxBooksAllowed;

    public DateTime MembershipExpiry
    {
        get => _membershipExpiry;
        set
        {
            if (value < DateTime.Now)
                throw new ArgumentException("Membership expiry date cannot be in the past");
            _membershipExpiry = value;
        }
    }

    public int MaxBooksAllowed
    {
        get => _maxBooksAllowed;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Maximum books allowed must be greater than 0");
            _maxBooksAllowed = value;
        }
    }

    public bool IsMembershipValid => DateTime.Now <= MembershipExpiry;

    public PremiumMember(string memberId, string name, string email) 
        : base(memberId, name, email)
    {
        MembershipExpiry = DateTime.Now.AddYears(1);
        MaxBooksAllowed = 5;  // Default value
    }

    public PremiumMember(string memberId, string name, string email, DateTime membershipExpiry, int maxBooksAllowed) 
        : base(memberId, name, email)
    {
        MembershipExpiry = membershipExpiry;
        MaxBooksAllowed = maxBooksAllowed;
    }

    public override void BorrowBook(Book book)
    {
        if (!IsMembershipValid)
            throw new InvalidOperationException("Premium membership has expired");
        
        if (BorrowedBooks.Count >= MaxBooksAllowed)
            throw new InvalidOperationException($"Cannot borrow more than {MaxBooksAllowed} books");

        base.BorrowBook(book);
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Membership Type: Premium");
        Console.WriteLine($"Membership Expiry: {MembershipExpiry:d}");
        Console.WriteLine($"Maximum Books Allowed: {MaxBooksAllowed}");
        Console.WriteLine($"Membership Status: {(IsMembershipValid ? "Active" : "Expired")}");
        Console.WriteLine($"Books Available to Borrow: {MaxBooksAllowed - BorrowedBooks.Count}");
    }
}