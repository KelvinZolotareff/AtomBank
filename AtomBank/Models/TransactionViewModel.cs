using System.Collections.Generic;

namespace AtomBank.Models
{
    public class TransactionViewModel
    {
        public class Transaction
        {

            public int Id { get; set; }
            public decimal Amount { get; set; }
            public bool IsIncome { get; set; }

            public string? TransactionDescription { get; set; }

            public DateTime TransactionDate { get; set; }

            public int TypeId { get; set; }
            public DateTime InclusionDate { get; set; }

        }
        public  List<Transaction> Transactions { get; set; }
    }
}
