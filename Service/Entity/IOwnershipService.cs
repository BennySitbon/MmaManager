namespace Service.Entity
{
    public interface IOwnershipService
    {
        decimal GetNetIncome(int ownershipId);
        string GetOwnershipFightRecord(int ownershipID);
        void PutOwnershipForSale(int ownershipId, int priceRequested);
    }
}