namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources.StockLocators;

    public class StockLocatorPricesService : IStockLocatorPricesService
    {
        private readonly IStockLocatorService domainService;

        public StockLocatorPricesService(IStockLocatorService domainService)
        {
            this.domainService = domainService;
        }

        public IResult<IEnumerable<StockLocatorPrices>> GetPrices(StockLocatorResource queryResource)
        {
            var querydate = queryResource.StockRotationDate == null 
                                ? (DateTime?)null 
                                : DateTime.Parse(queryResource.StockRotationDate);
            
            return new SuccessResult<IEnumerable<StockLocatorPrices>>(
                this.domainService.GetPrices(
                    queryResource.PalletNumber,
                    queryResource.PartNumber,
                    queryResource.LocationName,
                    queryResource.State,
                    queryResource.Category,
                    queryResource.StockPoolCode,
                    queryResource.BatchRef,
                    querydate));
        }
    }
}
