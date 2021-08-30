namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Wand;
    using Linn.Stores.Domain.LinnApps.Wand.Models;
    using Linn.Stores.Resources.Wand;

    public class WandItemsResourceBuilder : IResourceBuilder<IEnumerable<WandItem>>
    {
        public IEnumerable<WandItemResource> Build(IEnumerable<WandItem> wandItems)
        {
            return wandItems?.Select(
                w => new WandItemResource
                         {
                             ConsignmentId = w.ConsignmentId,
                             PartNumber = w.PartNumber,
                             PartDescription = w.PartDescription,
                             Quantity = w.Quantity,
                             QuantityScanned = w.QuantityScanned,
                             OrderNumber = w.OrderNumber,
                             OrderLine = w.OrderLine,
                             CountryCode = w.CountryCode,
                             LinnBarCode = w.LinnBarCode,
                             RequisitionNumber = w.RequisitionNumber,
                             RequisitionLine = w.RequisitionLine,
                             AllWanded = w.AllWanded == "Y",
                             BoxesPerProduct = w.BoxesPerProduct,
                             FunctionCode = w.FunctionCode,
                             BoxesWanded =
                                 string.IsNullOrWhiteSpace(w.BoxesWanded) ? null : this.GetBoxesWanded(w.BoxesWanded),
                             TypeOfSerialNumber = w.TypeOfSerialNumber,
                             WandStringSuggestion = WandService.WandStringSuggestion(
                                 w.TypeOfSerialNumber,
                                 w.BoxesPerProduct,
                                 w.Quantity,
                                 w.LinnBarCode)
                         });
        }

        public string GetLocation(IEnumerable<WandItem> model)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<IEnumerable<WandItem>>.Build(IEnumerable<WandItem> wandItems) =>
            this.Build(wandItems);

        private IEnumerable<int> GetBoxesWanded(string boxesWanded)
        {
            return boxesWanded.Select(ch => ch - '0').ToArray();
        }
    }
}
