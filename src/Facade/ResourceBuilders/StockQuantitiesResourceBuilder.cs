namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources.StockLocators;

    public class StockQuantitiesResourceBuilder : IResourceBuilder<StockQuantities>
    {
        public StockQuantitiesResource Build(StockQuantities model)
        {
            return new StockQuantitiesResource
                       {
                           DistributorStock = model.DistributorStock,
                           DistributorStockAllocated = model.DistributorStockAllocated,
                           FaultyStock = model.FaultyStock,
                           FaultyStockAllocated = model.FaultyStockAllocated,
                           GoodStock = model.GoodStock,
                           GoodStockAllocated = model.GoodStockAllocated,
                           OtherStock = model.OtherStock,
                           OtherStockAllocated = model.OtherStockAllocated,
                           PartNumber = model.PartNumber,
                           SupplierStock = model.SupplierStock,
                           SupplierStockAllocated = model.SupplierStockAllocated,
                           UninspectedStock = model.UninspectedStock,
                           UninspectedStockAllocated = model.UninspectedStockAllocated
                       };
        }

        object IResourceBuilder<StockQuantities>.Build(StockQuantities template) => this.Build(template);

        public string GetLocation(StockQuantities model)
        {
            throw new System.NotImplementedException();
        }
    }
}
