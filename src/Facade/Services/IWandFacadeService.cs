namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Wand.Models;
    using Linn.Stores.Resources.Wand;

    public interface IWandFacadeService
    {
        IResult<IEnumerable<WandConsignment>> GetWandConsignments();

        IResult<IEnumerable<WandItem>> GetWandItems(int consignmentId);

        IResult<WandResult> WandItem(WandItemRequestResource resource);
    }
}
