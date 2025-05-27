namespace Linn.Stores.Domain.LinnApps
{
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.Wcs;

    public interface IWarehouseService
    {
        MessageResult MoveAllPalletsToUpper();

        MessageResult MovePalletToUpper(int palletNumber, string reference);

        string GetPalletLocation(int palletNumber);

        int? GetPalletAtLocation(string location);

        WarehouseLocation GetWarehouseLocation(string location, int? palletNumber);

        bool MovePallet(int palletNumber, string destination, int priority, Employee who);

        bool AtMovePallet(int palletNumber, string fromLocation, string destination, int priority, Employee who);

        bool EmptyLocation(int palletNumber, string location, int priority, Employee who);

        IEnumerable<WarehouseLocation> GetWarehouseLocationsWithPallets();
    }
}
