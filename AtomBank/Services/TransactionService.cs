﻿using AtomBank.Data;
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

        public async Task<List<Transaction>> GetAllTransactionsAsync(int? month, int? year)
        {
            return await _context.Transactions.OrderByDescending(a => a.Amount).Where(a => a.Description == "Assinatura").ToListAsync();
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
