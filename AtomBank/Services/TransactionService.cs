using AtomBank.Data;
using AtomBank.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AtomBank.Services
{
    public class TransactionService
    {
        private readonly ApplicationDbContext _context;

        public TransactionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddTransactionAsync(decimal amount, bool isIncome, DateTime date, string description)
        {
            var transaction = new Transaction
            {
                Amount = amount,
                IsIncome = isIncome,
                Date = date,
                Description = description
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync(string order, string orderDirection)
        {
            IQueryable<Transaction> query = _context.Transactions;

            switch (order)
            {
                case "Valor":
                    query = (orderDirection == "asc") ? query.OrderBy(a => a.Amount) : query.OrderByDescending(a => a.Amount);
                    break;
                case "Data":
                    query = (orderDirection == "asc") ? query.OrderBy(a => a.Date) : query.OrderByDescending(a => a.Date);
                    break;
                default:
                    query = query.OrderBy(a => a.Date);
                    break;
            }

            return await query.ToListAsync();
        }

        public async Task<decimal> GetTotalIncomeForMonthAsync(int year, int month)
        {
            return await _context.Transactions
                .Where(t => t.IsIncome && t.Date.Year == year && t.Date.Month == month)
                .SumAsync(t => t.Amount);
        }

        public async Task<decimal> GetTotalExpenseForMonthAsync(int year, int month)
        {
            return await _context.Transactions
                .Where(t => !t.IsIncome && t.Date.Year == year && t.Date.Month == month)
                .SumAsync(t => t.Amount);
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
