namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class PartStorageTypeResourceBuilder : IResourceBuilder<PartStorageType>
    {
        public PartStorageTypeResource Build(PartStorageType partStorageType)
        {
            return new PartStorageTypeResource
            {
                Id = partStorageType.Id,
                PartNumber = partStorageType.PartNumber,
                StorageType = partStorageType.StorageType,
                Maximum = partStorageType.Maximum,
                Increment = partStorageType.Increment,
                Preference = partStorageType.Preference,
                Remarks = partStorageType.Remarks
            };
        }

        public string GetLocation(PartStorageType p)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<PartStorageType>.Build(PartStorageType partStorageType) => this.Build(partStorageType);
    }
}
