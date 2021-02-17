namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Wand.Models;

    public interface IWandFacadeService
    {
        IResult<IEnumerable<WandConsignment>> GetWandConsignments();

        IResult<IEnumerable<WandItem>> GetWandItems(int consignmentId);
    }
}
