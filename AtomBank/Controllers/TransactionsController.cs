﻿using AtomBank.Models;
using AtomBank.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AtomBank.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly TransactionService _transactionService;

        public TransactionsController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        [Route("Home/AddTransaction")]
        public async Task<IActionResult> AddTransaction(decimal amount, bool isIncome, DateTime date, string? description)
        {
            // Adiciona a transação
            await _transactionService.AddTransactionAsync(amount, isIncome, date, description);

            // Redireciona para uma página de confirmação ou a página inicial
            return RedirectToAction("Index", "Home");
        }

       
    }
}