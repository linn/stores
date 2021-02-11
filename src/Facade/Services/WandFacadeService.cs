namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Wand.Models;

    public class WandFacadeService : IWandFacadeService
    {
        public IResult<IEnumerable<WandConsignment>> GetWandConsignments()
        {
            throw new System.NotImplementedException();
        }
    }
}
