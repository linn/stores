namespace Linn.Stores.Domain.LinnApps.StockLocators
{
    using Linn.Common.Persistence;

    public interface IStoresPalletRepository : IRepository<StoresPallet, int>
    {
        void UpdatePallet(int id, string auditDepartmentCode, int? auditFrequencyWeeks);
    }
}
