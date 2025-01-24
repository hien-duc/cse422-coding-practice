using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab3_TranLeHienDuc_CSE422
{
    public abstract class Transaction
    {
        private string _transactionID;
        private DateTime _transactionDate;
        private Member _member;

        public string TransactionID
        {
            get { return _transactionID; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty or null");
                _transactionID = value;
            }
        }

        public DateTime TransactionDate
        {
            get { return _transactionDate; }
            set
            {
                if (value > DateTime.Now)
                {
                    throw new ArgumentException("Transaction date cannot be in the future");
                }
                _transactionDate = value;
            }
        }

        public Member Member
        {
            get { return _member; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Member cannot be null");
                }
                _member = value;
            }
        }

        public abstract void Execute();
    }
}
