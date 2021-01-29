namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public interface IPartsFacadeService : IFacadeService<Part, int, PartResource, PartResource>
    {
        IResult<IEnumerable<Part>> GetDeptStockPalletParts();
    }
}
