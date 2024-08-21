namespace AtomBank.Models
{
    public class TransactionsTypeViewModel
    {
        public class TransactionsType
        {

            public int Id { get; set; }
            public string? TypeName { get; set; }
            public bool IsFixed { get; set; }
            public string? TypeDescription { get; set; }
            public DateTime InclusionDate { get; set; }


        }
        public List<TransactionsType> Transactions_Types { get; set; }
    }
}
