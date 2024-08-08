using System.ComponentModel.DataAnnotations;

namespace AtomBank.Models
{
    public class Transaction
    {
          
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public bool IsIncome { get; set; } 
        public DateTime Date { get; set; }
        public string? Description { get; set; } 
    
    }
}
