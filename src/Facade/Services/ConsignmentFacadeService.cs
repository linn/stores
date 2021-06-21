namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;

    public class ConsignmentFacadeService : FacadeService<Consignment, int, ConsignmentResource, ConsignmentUpdateResource>
    {
        public ConsignmentFacadeService(IRepository<Consignment, int> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override Consignment CreateFromResource(ConsignmentResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(Consignment entity, ConsignmentUpdateResource updateResource)
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

            foreach (var updatePallet in updateResource.Pallets)
            {
                var existingPallet =
                    entity.Pallets.FirstOrDefault(a => a.PalletNumber == updatePallet.PalletNumber);
                if (existingPallet == null)
                {
                    entity.Pallets.Add(new ConsignmentPallet
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
                    e => updateResource.Pallets.Select(a => a.PalletNumber).Contains(e.PalletNumber) == false).ToList();
                foreach (var removedPallet in removedPallets)
                {
                    entity.Pallets.RemoveAt(entity.Pallets.IndexOf(removedPallet));
                }
            }
        }

        protected override Expression<Func<Consignment, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
