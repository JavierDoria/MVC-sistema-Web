using MoneyFlow.Context;
using MoneyFlow.DTO;
using MoneyFlow.Models.Entities;

namespace MoneyFlow.Managers
{
    public class TransactionManager (AppDbContext _dbContext)
    {
        public int New(TransactionDTO modelDto)
        {
            var entity = new Transaction
            {
                Date = modelDto.Date,
                TotalAmount = modelDto.TotalAmount,
                Comment = modelDto.Comment,
                ServiceId = modelDto.ServiceId,
                UserId = modelDto.UserId,
            };
            _dbContext.Transaction.Add(entity);
            var affected_rows = _dbContext.SaveChanges();
            return affected_rows;
        }
        public List<HistoryDto> GetHistory(DateOnly startDate, DateOnly EndDate, int UserId) 
        { 
            var list = _dbContext.Transaction
                .Where (item =>
                item.UserId == UserId &&
                item.Date >= startDate && item.Date <= EndDate).Select(item => new HistoryDto
                {
                    Date = item.Date.ToString("dd/MM/yyyy"),
                    Month = item.Date.ToString("MMMM"),
                    TypeService = item.Service.Type,
                    Service = item.Service.Name,
                    TotalAmount = item.TotalAmount.ToString()
                }).ToList();
            return list;
        }
    }
}
