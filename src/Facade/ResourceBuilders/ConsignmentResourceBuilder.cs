namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;

    public class ConsignmentResourceBuilder : IResourceBuilder<Consignment>
    {
        public ConsignmentResource Build(Consignment consignment)
        {
            var addressBuilder = new AddressResourceBuilder();
            var employeeBuilder = new EmployeeResourceBuilder();

            return new ConsignmentResource
                       {
                           ConsignmentId = consignment.ConsignmentId,
                           SalesAccountId = consignment.SalesAccountId,
                           AddressId = consignment.AddressId,
                           Address = consignment.Address == null ? null : addressBuilder.Build(consignment.Address),
                           Status = consignment.Status,
                           Carrier = consignment.Carrier,
                           ClosedBy = consignment.ClosedBy == null ? null : employeeBuilder.Build(consignment.ClosedBy),
                           CustomerName = consignment.CustomerName,
                           DateOpened = consignment.DateOpened.ToString("o"),
                           DateClosed = consignment.DateClosed?.ToString("o"),
                           DespatchLocationCode = consignment.DespatchLocationCode,
                           HubId = consignment.HubId,
                           ShippingMethod = consignment.ShippingMethod,
                           Terms = consignment.Terms,
                           Warehouse = consignment.Warehouse,
                           CustomsEntryCodePrefix = consignment.CustomsEntryCodePrefix,
                           CustomsEntryCode = consignment.CustomsEntryCode,
                           CustomsEntryCodeDate = consignment.CustomsEntryCodeDate?.ToString("o"),
                           CarrierRef = consignment.CarrierRef,
                           MasterCarrierRef = consignment.MasterCarrierRef,
                           Pallets = consignment.Pallets?.Select(
                               pallet => new ConsignmentPalletResource
                                             {
                                                 ConsignmentId = pallet.ConsignmentId,
                                                 PalletNumber = pallet.PalletNumber,
                                                 Weight = pallet.Weight,
                                                 Depth = pallet.Depth,
                                                 Height = pallet.Height,
                                                 Width = pallet.Width
                                             }),
                           Items = consignment.Items?.Select(
                               item => new ConsignmentItemResource
                                           {
                                                ConsignmentId = item.ConsignmentId,
                                                ItemNumber = item.ItemNumber,
                                                ItemType = item.ItemType,
                                                Quantity = item.Quantity,
                                                SerialNumber = item.SerialNumber,
                                                Weight = item.Weight,
                                                Width = item.Width,
                                                Height = item.Height,
                                                Depth = item.Depth,
                                                ContainerNumber = item.ContainerNumber,
                                                PalletNumber = item.PalletNumber,
                                                ContainerType = item.ContainerType,
                                                MaybeHalfAPair = item.MaybeHalfAPair,
                                                OrderNumber = item.OrderNumber,
                                                OrderLine = item.OrderLine,
                                                ItemBaseWeight = item.ItemBaseWeight,
                                                ItemDescription = item.ItemDescription,
                                                RsnNumber = item.RsnNumber
                                           }),
                           Invoices = consignment.Invoices?.Select(
                               inv => new InvoiceResource
                                             {
                                                 ConsignmentId = inv.ConsignmentId,
                                                 DocumentType = inv.DocumentType,
                                                 DocumentNumber = inv.DocumentNumber
                                             }),
                           ExportBooks = consignment.ExportBooks?.Select(
                               exportBook => new ExportBookResource
                                             {
                                                 ConsignmentId = exportBook.ConsignmentId,
                                                 ExportId = exportBook.ExportId
                                             }),
                           Links = this.BuildLinks(consignment).ToArray()
                       };
        }

        public string GetLocation(Consignment consignment)
        {
            return $"/logistics/consignments/{consignment.ConsignmentId}";
        }

        object IResourceBuilder<Consignment>.Build(Consignment consignment) => this.Build(consignment);

        private IEnumerable<LinkResource> BuildLinks(Consignment consignment)
        {
            yield return new LinkResource("self", this.GetLocation(consignment));

            if (consignment.HubId.HasValue)
            {
                yield return new LinkResource("hub", $"/logistics/hubs/{consignment.HubId}");
            }

            if (!string.IsNullOrEmpty(consignment.Carrier))
            {
                yield return new LinkResource("carrier", $"/logistics/carriers/{consignment.Carrier}");
            }

            if (!string.IsNullOrEmpty(consignment.Terms))
            {
                yield return new LinkResource("shipping-term", $"/logistics/shipping-terms?searchTerm={consignment.Terms}&exactOnly=true");
            }
        }
    }
}
