using AtomBank.Models;
using AtomBank.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AtomBank.Controllers
{
    public class HomeController : Controller
    {
        private readonly TransactionService _transactionService;

        public HomeController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<IActionResult> Index(int? month, int? year, string order, string orderDirection)
        {
            if (string.IsNullOrEmpty(order))
            {
                order = "desc"; // Ordena por valor decrescente por padrão
            }

            var transactions = await _transactionService.GetAllTransactionsAsync(order, orderDirection);

            if (month.HasValue && year.HasValue)
            {
                transactions = transactions.Where(t => t.Date.Month == month.Value && t.Date.Year == year.Value).ToList();
            }

            var totalIncome = transactions.Where(t => t.IsIncome).Sum(t => t.Amount);
            var totalExpense = transactions.Where(t => !t.IsIncome).Sum(t => t.Amount);
            var total = totalIncome - totalExpense;

            var transactionViewModel = new TransactionViewModel
            {
                Transactions = transactions
            };

            var totalViewModel = new TotalViewModel
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                Total = total
            };

            var model = new CombinedViewModel
            {
                TransactionViewModel = transactionViewModel,
                TotalViewModel = totalViewModel
            };


            ViewData["Order"] = order ?? "Valor";
            ViewData["OrderDirection"] = orderDirection ?? "asc";
            ViewData["Month"] = month;
            ViewData["Year"] = year;

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> GetMonthlyTotals(int year, int month)
        {
            var totalIncome = await _transactionService.GetTotalIncomeForMonthAsync(year, month);
            var totalExpense = await _transactionService.GetTotalExpenseForMonthAsync(year, month);
            var total = totalIncome - totalExpense;

            var totalViewModel = new TotalViewModel
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                Total = total
            };

            return Ok(totalViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            await _transactionService.DeleteTransactionAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
