namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.RequestResources;

    public interface IParcelService : IFacadeService<Parcel, int, ParcelResource, ParcelResource>
    {
        IResult<IEnumerable<Parcel>> Search(ParcelSearchRequestResource resource);
    }
}
