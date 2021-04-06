namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class ExportReturnResourceBuilder : IResourceBuilder<ExportReturn>
    {
        private readonly ExportReturnDetailResourceBuilder exportReturnDetailResourceBuilder =
            new ExportReturnDetailResourceBuilder();

        private readonly EmployeeResourceBuilder employeeResourceBuilder = new EmployeeResourceBuilder();

        private readonly SalesOutletResourceBuilder salesOutletResourceBuilder = new SalesOutletResourceBuilder();

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
                           MadeIntercompanyInvoices = exportReturn.MadeIntercompanyInvoices,
                           DateProcessed = exportReturn.DateProcessed?.ToString("o"),
                           ReturnForCredit = exportReturn.ReturnForCredit,
                           ExportCustomsEntryCode = exportReturn.ExportCustomsEntryCode,
                           ExportCustomsCodeDate = exportReturn.ExportCustomsCodeDate?.ToString("o"),
                           RaisedBy =
                               exportReturn.RaisedBy != null
                                   ? this.employeeResourceBuilder.Build(exportReturn.RaisedBy)
                                   : null,
                           SalesOutlet =
                               exportReturn.SalesOutlet != null
                                   ? this.salesOutletResourceBuilder.Build(exportReturn.SalesOutlet)
                                   : null,
                           ExportReturnDetails = exportReturn.ExportReturnDetails?.Select(
                               e => this.exportReturnDetailResourceBuilder.Build(e)),
                           Links = this.BuildLinks(exportReturn).ToArray()
                       };
        }

        public string GetLocation(ExportReturn model)
        {
            return $"/inventory/exports/returns/{model.ReturnId}";
        }

        object IResourceBuilder<ExportReturn>.Build(ExportReturn exportReturn) => this.Build(exportReturn);

        private IEnumerable<LinkResource> BuildLinks(ExportReturn exportReturn)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(exportReturn) };
        }
    }
}