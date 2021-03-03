namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class ExportReturnResourceBuilder : IResourceBuilder<ExportReturn>
    {
        private readonly ExportReturnDetailResourceBuilder exportReturnDetailResourceBuilder =
            new ExportReturnDetailResourceBuilder();

        public ExportReturnResource Build(ExportReturn exportReturn)
        {
            return new ExportReturnResource
                       {
                           CarrierCode = exportReturn.CarrierCode,
                           ReturnId = exportReturn.ReturnId,
                           DateCreated = exportReturn.DateCreated.ToString("o"),
                           Currency = exportReturn.Currency,
                           AccountId = exportReturn.AccountId,
                           HubId = exportReturn.HubId,
                           OutletNumber = exportReturn.OutletNumber,
                           DateDispatched = exportReturn.DateDispatched?.ToString("o"),
                           DateCancelled = exportReturn.DateCancelled?.ToString("o"),
                           CarrierRef = exportReturn.CarrierRef,
                           Terms = exportReturn.Terms,
                           NumPallets = exportReturn.NumPallets,
                           NumCartons = exportReturn.NumCartons,
                           GrossWeightKg = exportReturn.GrossWeightKg,
                           GrossDimsM3 = exportReturn.GrossDimsM3,
                           RaisedBy = exportReturn.RaisedBy,
                           ExportReturnDetails = exportReturn.ExportReturnDetails?.Select(
                               e => this.exportReturnDetailResourceBuilder.Build(e))
                       };
        }

        public string GetLocation(ExportReturn model)
        {
            throw new System.NotImplementedException();
        }

        object IResourceBuilder<ExportReturn>.Build(ExportReturn exportReturn) => this.Build(exportReturn);
    }
}