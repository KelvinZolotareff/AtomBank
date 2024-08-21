using Microsoft.EntityFrameworkCore;
using static AtomBank.Models.TransactionViewModel;
using static AtomBank.Models.TransactionsTypeViewModel;

namespace AtomBank.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionsType> Transactions_Types { get; set; }


    }
}
