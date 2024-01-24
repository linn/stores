namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Models;
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.Wcs;
    using Linn.Stores.Resources;
    using Linn.Stores.Domain.LinnApps.Scs;
    using Linn.Stores.Resources.Scs;

    public interface IWarehouseFacadeService
    {
        IResult<MessageResult> MoveAllPalletsToUpper();

        IResult<MessageResult> MovePalletToUpper(int palletNumber, string reference);

        IResult<MessageResult> MakeWarehouseTask(WarehouseTaskResource resource);

        IResult<WarehouseLocation> GetPalletLocation(int palletNumber);

        IResult<WarehouseLocation> GetPalletAtLocation(string location);

        IResult<IEnumerable<ScsPallet>> GetScsPallets();

        IResult<MessageResult> StorePallets(ScsPalletsResource resource);
    }
}
