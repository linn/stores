namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments.Models;
    using Linn.Stores.Resources.Consignments;

    public class PackingListResourceBuilder : IResourceBuilder<PackingList>
    {
        public PackingListResource Build(PackingList model)
        {
            return new PackingListResource
                       {
                           ConsignmentId = model.ConsignmentId,
                           DeliveryAddress = Utilities.GetPrintAddress(model.DeliveryAddress),
                           SenderAddress = Utilities.GetPrintAddress(model.SenderAddress),
                           DespatchDate = model.DespatchDate?.ToString("o"),
                           Items = model.Items?.OrderBy(o => o.ItemNumber)
                               .Select(i => new PackingListItemResource
                                                {
                                                    ContainerNumber = i.ContainerNumber,
                                                    Description = i.Description,
                                                    ItemNumber = i.ItemNumber,
                                                    Weight = $"{i.Weight} Kgs",
                                                    DisplayDimensions = i.DisplayDimensions
                                                }),
                           Pallets = model.Pallets?.OrderBy(o => o.PalletNumber)
                               .Select(p => new PackingListPalletResource
                                                {
                                                    DisplayDimensions = p.DisplayDimensions,
                                                    DisplayWeight = p.DisplayWeight,
                                                    PalletNumber = p.PalletNumber,
                                                    Volume = p.Volume,
                                                    Items = p.Items?.Select(i => new PackingListItemResource
                                                        {
                                                            ContainerNumber = i.ContainerNumber,
                                                            Description = i.Description,
                                                            ItemNumber = i.ItemNumber,
                                                            Weight = $"{i.Weight} Kgs",
                                                            DisplayDimensions = i.DisplayDimensions
                                                    })
                                                }),
                           NumberOfItemsNotOnPallets = model.NumberOfItemsNotOnPallets,
                           NumberOfPallets = model.NumberOfPallets,
                           TotalGrossWeight = $"{model.TotalGrossWeight} Kgs",
                           TotalVolume = $"{model.TotalVolume} m3"
                       };
        }

        object IResourceBuilder<PackingList>.Build(PackingList model) => this.Build(model);

        public string GetLocation(PackingList model)
        {
            throw new System.NotImplementedException();
        }
    }
}
