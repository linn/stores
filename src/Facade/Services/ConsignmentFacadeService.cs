namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Resources.Consignments;
    using Linn.Stores.Resources.RequestResources;

    public class ConsignmentFacadeService : FacadeService<Consignment, int, ConsignmentResource, ConsignmentUpdateResource>, IConsignmentFacadeService
    {
        private readonly IConsignmentService consignmentService;

        private readonly IDatabaseService databaseService;

        private readonly IRepository<Consignment, int> repository;

        private readonly IRepository<Invoice, int> invoiceRepository;

        public ConsignmentFacadeService(
            IRepository<Consignment, int> repository,
            ITransactionManager transactionManager,
            IConsignmentService consignmentService,
            IDatabaseService databaseService,
            IRepository<Invoice, int> invoiceRepository)
            : base(repository, transactionManager)
        {
            this.consignmentService = consignmentService;
            this.databaseService = databaseService;
            this.repository = repository;
            this.invoiceRepository = invoiceRepository;
        }

        protected override Consignment CreateFromResource(ConsignmentResource resource)
        {
            var consignment = new Consignment
                       {
                           Carrier = resource.Carrier,
                           Terms = resource.Terms,
                           HubId = resource.HubId,
                           ShippingMethod = resource.ShippingMethod,
                           DespatchLocationCode = resource.DespatchLocationCode,
                           CustomsEntryCodePrefix = resource.CustomsEntryCodePrefix,
                           CustomsEntryCode = resource.CustomsEntryCode,
                           CustomsEntryCodeDate = string.IsNullOrEmpty(resource.CustomsEntryCodeDate)
                                                      ? (DateTime?)null
                                                      : DateTime.Parse(resource.CustomsEntryCodeDate),
                           AddressId = resource.AddressId,
                           CustomerName = resource.CustomerName,
                           DateOpened = DateTime.Now,
                           SalesAccountId = resource.SalesAccountId,
                           Pallets = new List<ConsignmentPallet>(),
                           Items = new List<ConsignmentItem>(),
                           Status = resource.Status,
                           ConsignmentId = this.databaseService.GetNextVal("CONS_SEQ")
                       };
            this.UpdatePallets(consignment, resource.Pallets.ToList());
            this.UpdateItems(consignment, resource.Items.ToList());

            return consignment;
        }

        protected override void UpdateFromResource(Consignment entity, ConsignmentUpdateResource updateResource)
        {
            if (updateResource.Status == "C")
            {
                if (entity.Status == "L")
                {
                    if (!updateResource.ClosedById.HasValue)
                    {
                        throw new ConsignmentCloseException("Closed by id must be provided to close consignment");
                    }

                    this.consignmentService.CloseConsignment(entity, updateResource.ClosedById.Value);

                    entity.ClosedById = updateResource.ClosedById;
                    entity.Status = "C";
                    entity.DateClosed = DateTime.Now;
                }
                else
                {
                    // only update customs fields
                    entity.CustomsEntryCodePrefix = updateResource.CustomsEntryCodePrefix;
                    entity.CustomsEntryCode = updateResource.CustomsEntryCode;
                    entity.CustomsEntryCodeDate = string.IsNullOrEmpty(updateResource.CustomsEntryCodeDate)
                                                      ? (DateTime?)null
                                                      : DateTime.Parse(updateResource.CustomsEntryCodeDate);
                    entity.CarrierRef = updateResource.CarrierRef;
                    entity.MasterCarrierRef = updateResource.MasterCarrierRef;
                }

            }
            else
            {
                entity.Carrier = updateResource.Carrier;
                entity.Terms = updateResource.Terms;
                entity.HubId = entity.Address?.Country?.ECMember == "Y" ? updateResource.HubId : null;
                entity.ShippingMethod = updateResource.ShippingMethod;
                entity.DespatchLocationCode = updateResource.DespatchLocationCode;
                entity.CustomsEntryCodePrefix = updateResource.CustomsEntryCodePrefix;
                entity.CustomsEntryCode = updateResource.CustomsEntryCode;
                entity.CustomsEntryCodeDate = string.IsNullOrEmpty(updateResource.CustomsEntryCodeDate)
                                                  ? (DateTime?)null
                                                  : DateTime.Parse(updateResource.CustomsEntryCodeDate);
                entity.CarrierRef = updateResource.CarrierRef;
                entity.MasterCarrierRef = updateResource.MasterCarrierRef;

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

        public IResult<IEnumerable<Consignment>> GetByRequestResource(ConsignmentsRequestResource resource)
        {
            if (!string.IsNullOrEmpty(resource.From))
            {
                if (string.IsNullOrEmpty(resource.To))
                {
                    return new BadRequestResult<IEnumerable<Consignment>>("No To Date specified");
                }

                var fromDate = DateTime.Parse(resource.From);
                var toDate = DateTime.Parse(resource.To);

                if (resource.HubId == null)
                {
                    return new SuccessResult<IEnumerable<Consignment>>(this.repository.FilterBy(c => c.DateClosed >= fromDate && c.DateClosed <= toDate));
                }
                else
                {
                    return new SuccessResult<IEnumerable<Consignment>>(this.repository.FilterBy(c => c.DateClosed >= fromDate && c.DateClosed <= toDate && c.HubId == resource.HubId));
                }
            }

            return this.GetAll();
        }

        public IResult<IEnumerable<Consignment>> GetByInvoiceNumber(int invoiceNumber)
        {
            var invoice = this.invoiceRepository.FindById(invoiceNumber);
            if (invoice == null)
            {
                return new NotFoundResult<IEnumerable<Consignment>>("Could not find invoice");
            }

            if (invoice.ConsignmentId == null)
            {
                return new NotFoundResult<IEnumerable<Consignment>>("Invoice does not have consignment");
            }

            var consignment = this.repository.FindById(invoice.ConsignmentId.Value);

            if (consignment == null)
            {
                return new NotFoundResult<IEnumerable<Consignment>>("Consignment does not exist");
            }

            return new SuccessResult<IEnumerable<Consignment>>(new List<Consignment>() { consignment });
        }
    }
}
