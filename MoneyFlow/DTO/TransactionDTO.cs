using MoneyFlow.Models.Entities;

namespace MoneyFlow.DTO
{
    public class TransactionDTO
    {
        public int ServiceId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public DateOnly Date { get; set; }

        public decimal TotalAmount { get; set; }
     
    }
}
