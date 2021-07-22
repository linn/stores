namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Resources.Consignments;

    public class ConsignmentFacadeService : FacadeService<Consignment, int, ConsignmentResource, ConsignmentUpdateResource>
    {
        private readonly IConsignmentService consignmentService;

        public ConsignmentFacadeService(
            IRepository<Consignment, int> repository,
            ITransactionManager transactionManager,
            IConsignmentService consignmentService)
            : base(repository, transactionManager)
        {
            this.consignmentService = consignmentService;
        }

        protected override Consignment CreateFromResource(ConsignmentResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(Consignment entity, ConsignmentUpdateResource updateResource)
        {
            if (updateResource.Status == "C" && entity.Status == "L")
            {
                if (!updateResource.ClosedById.HasValue)
                {
                    throw new ConsignmentCloseException("Closed by id must be provided to close consignment");
                }

                this.consignmentService.CloseConsignment(entity, updateResource.ClosedById.Value);

                entity.ClosedById = updateResource.ClosedById;
                entity.Status = "C";
            }
            else
            {
                entity.Carrier = updateResource.Carrier;
                entity.Terms = updateResource.Terms;
                entity.HubId = updateResource.HubId;
                entity.ShippingMethod = updateResource.ShippingMethod;
                entity.DespatchLocationCode = updateResource.DespatchLocationCode;
                entity.CustomsEntryCodePrefix = updateResource.CustomsEntryCodePrefix;
                entity.CustomsEntryCode = updateResource.CustomsEntryCode;
                entity.CustomsEntryCodeDate = string.IsNullOrEmpty(updateResource.CustomsEntryCodeDate)
                                                  ? (DateTime?)null
                                                  : DateTime.Parse(updateResource.CustomsEntryCodeDate);

                this.UpdatePallets(entity, updateResource.Pallets.ToList());
                this.UpdateItems(entity, updateResource.Items.ToList());
            }
        }

        protected override Expression<Func<Consignment, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }

        private void UpdatePallets(Consignment entity, IList<ConsignmentPalletResource> updatePallets)
        {
            foreach (var updatePallet in updatePallets)
            {
                var existingPallet = entity.Pallets.FirstOrDefault(a => a.PalletNumber == updatePallet.PalletNumber);
                if (existingPallet == null)
                {
                    entity.Pallets.Add(
                        new ConsignmentPallet
                            {
                                ConsignmentId = entity.ConsignmentId,
                                PalletNumber = updatePallet.PalletNumber,
                                Weight = updatePallet.Weight,
                                Width = updatePallet.Width,
                                Height = updatePallet.Height,
                                Depth = updatePallet.Depth
                            });
                }
                else
                {
                    existingPallet.Weight = updatePallet.Weight;
                    existingPallet.Height = updatePallet.Height;
                    existingPallet.Width = updatePallet.Width;
                    existingPallet.Depth = updatePallet.Depth;
                }

                var removedPallets = entity.Pallets.Where(
                    e => updatePallets.Select(a => a.PalletNumber).Contains(e.PalletNumber) == false).ToList();
                foreach (var removedPallet in removedPallets)
                {
                    entity.Pallets.RemoveAt(entity.Pallets.IndexOf(removedPallet));
                }
            }
        }

        private void UpdateItems(Consignment entity, IList<ConsignmentItemResource> updateItems)
        {
            foreach (var itemResource in updateItems)
            {
                var existingItem = entity.Items.FirstOrDefault(a => a.ItemNumber == itemResource.ItemNumber);
                if (existingItem == null)
                {
                    entity.Items.Add(
                        new ConsignmentItem
                            {
                                ConsignmentId = entity.ConsignmentId,
                                ItemNumber = itemResource.ItemNumber,
                                ItemBaseWeight = itemResource.ItemBaseWeight,
                                PalletNumber = itemResource.PalletNumber,
                                ContainerNumber = itemResource.ContainerNumber,
                                ContainerType = itemResource.ContainerType,
                                ItemDescription = itemResource.ItemDescription,
                                ItemType = itemResource.ItemType,
                                MaybeHalfAPair = itemResource.MaybeHalfAPair,
                                OrderLine = itemResource.OrderLine,
                                OrderNumber = itemResource.OrderNumber,
                                RsnNumber = itemResource.RsnNumber,
                                Quantity = itemResource.Quantity,
                                SerialNumber = itemResource.SerialNumber,
                                Weight = itemResource.Weight,
                                Width = itemResource.Width,
                                Height = itemResource.Height,
                                Depth = itemResource.Depth
                            });
                }
                else
                {
                    existingItem.Weight = itemResource.Weight;
                    existingItem.Height = itemResource.Height;
                    existingItem.Width = itemResource.Width;
                    existingItem.Depth = itemResource.Depth;
                    existingItem.PalletNumber = itemResource.PalletNumber;
                    existingItem.ContainerNumber = itemResource.ContainerNumber;
                    existingItem.ContainerType = itemResource.ContainerType;
                    existingItem.ItemDescription = itemResource.ItemDescription;
                    existingItem.ItemType = itemResource.ItemType;
                    existingItem.MaybeHalfAPair = itemResource.MaybeHalfAPair;
                    existingItem.OrderLine = itemResource.OrderLine;
                    existingItem.OrderNumber = itemResource.OrderNumber;
                    existingItem.RsnNumber = itemResource.RsnNumber;
                    existingItem.Quantity = itemResource.Quantity;
                    existingItem.SerialNumber = itemResource.SerialNumber;
                    existingItem.Weight = itemResource.Weight;
                    existingItem.Width = itemResource.Width;
                    existingItem.Height = itemResource.Height;
                    existingItem.Depth = itemResource.Depth;
                    existingItem.ItemBaseWeight = itemResource.ItemBaseWeight;
                }

                var removedItems = entity.Items.Where(
                    e => updateItems.Select(a => a.ItemNumber).Contains(e.ItemNumber) == false).ToList();
                foreach (var removedItem in removedItems)
                {
                    entity.Items.RemoveAt(entity.Items.IndexOf(removedItem));
                }
            }
        }
    }
}
