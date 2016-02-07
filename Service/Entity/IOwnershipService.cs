namespace Service.Entity
{
    public interface IOwnershipService
    {
        decimal GetNetIncome(int ownershipId);
        string GetOwnershipRecord(int ownershipID);
        void SellOwnership(int ownershipId, decimal priceRequested);
    }
}