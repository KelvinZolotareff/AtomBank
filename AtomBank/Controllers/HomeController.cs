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
        public IActionResult Cards()
        {
            return View();
        }
        public async Task<IActionResult> Index(int? month, int? year)
        {
            
            int mes = month ?? DateTime.Today.Month;
            int ano = year ?? DateTime.Today.Year;

            var transactions = await _transactionService.GetAllTransactionsAsync(mes, ano);

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


            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            await _transactionService.DeleteTransactionAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
