using AtomBank.Data;
using AtomBank.Models;
using Microsoft.EntityFrameworkCore;

namespace AtomBank.Services
{
    public class TransactionService
    {
        private readonly ApplicationDbContext _context;

        public TransactionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddTransactionAsync(decimal amount, bool isIncome, DateTime date, string description, int type)
        {
            var transaction = new TransactionViewModel.Transaction
            {
                Amount = amount,
                IsIncome = isIncome,
                TransactionDescription = description,
                TransactionDate = date,
                TypeId = type,
                InclusionDate = DateTime.Now

            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TransactionViewModel.Transaction>> GetAllTransactionsAsync(int? month, int? year)
        {
            int[] tiposFixos = { 1, 2, 3 };

            return await _context.Transactions
                    .Where(a => (a.TransactionDate.Month == month && a.TransactionDate.Year == year)
                     || tiposFixos.Contains(a.TypeId))
                    .OrderByDescending(a => a.Amount)
                    .ToListAsync();
        }

        public async Task<List<TransactionsTypeViewModel.TransactionsType>> GetAllTransactionsTypesAsync()
        {

            return await _context.Transactions_Types.ToListAsync();
        }
        public async Task DeleteTransactionAsync(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
            }
        }
    }
}
