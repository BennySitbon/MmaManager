using System.Linq;
using Domain.DAL;
using Domain.Models;

namespace Service.Entity
{
    public class UserStatisticsService : IUserStatisticsService
    {
        private readonly IRepository _repository;

        public UserStatisticsService(IRepository repository)
        {
            _repository = repository;
        }

        public decimal GetUserWorth(string username)
        {
            decimal worth = 0;
            var transactions = _repository.GetAll<Transaction>(t => t.Where(transaction =>
                transaction.FromUser == username || transaction.ToUser == username).ToList());
            transactions.ForEach(t => worth+=t.AmountForUser(username));
            return worth;
        }
    }
}