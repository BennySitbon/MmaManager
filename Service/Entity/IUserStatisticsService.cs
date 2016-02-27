namespace Service.Entity
{
    public interface IUserStatisticsService
    {
        decimal GetUserWorth(string username);
    }
}