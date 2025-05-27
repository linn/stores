namespace Linn.Stores.Domain.LinnApps
{
    using System.Collections.Generic;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.Wcs;

    public class WarehouseService : IWarehouseService
    {
        private readonly IWcsPack wcsPack;

        private readonly IQueryRepository<WarehouseLocation> warehouseLocationRepository;

        public WarehouseService(IWcsPack wcsPack, IQueryRepository<WarehouseLocation> warehouseLocationRepository)
        {
            this.wcsPack = wcsPack;
            this.warehouseLocationRepository = warehouseLocationRepository;
        }

        public MessageResult MoveAllPalletsToUpper()
        {
            this.wcsPack.MoveDpqPalletsToUpper();
            return new MessageResult("Move pallets to upper called successfully");
        }

        public MessageResult MovePalletToUpper(int palletNumber, string reference)
        {
            if (!this.wcsPack.CanMovePalletToUpper(palletNumber))
            {
                return new MessageResult($"Pallet {palletNumber} can no longer be moved to upper");
            }

            this.wcsPack.MovePalletToUpper(palletNumber, reference);
            return new MessageResult($"Pallet {palletNumber} move to upper called successfully");
        }

        public string GetPalletLocation(int palletNumber)
        {
            return this.wcsPack.PalletLocation(palletNumber);
        }

        public int? GetPalletAtLocation(string location)
        {
            return this.wcsPack.PalletAtLocation(location);
        }

        public WarehouseLocation GetWarehouseLocation(string location, int? palletNumber)
        {
            var warehouseLocation = new WarehouseLocation() {Location = location, PalletId = palletNumber};

            if (string.IsNullOrEmpty(location) && palletNumber != null)
            {
                warehouseLocation.Location = this.wcsPack.PalletLocation((int) palletNumber);
            }
            else if (!string.IsNullOrEmpty(location) && palletNumber == null)
            {
                warehouseLocation.PalletId = this.wcsPack.PalletAtLocation(location);
            }
            else
            {
                return null;
            }
            
            return warehouseLocation;
        }

        public bool MovePallet(int palletNumber, string destination, int priority, Employee who)
        {
            var taskNo = this.wcsPack.MovePallet(palletNumber, destination, priority, "BK", who.Id);
            return taskNo > 0;
        }

        public bool AtMovePallet(int palletNumber, string fromLocation, string destination, int priority, Employee who)
        {
            var taskNo = this.wcsPack.AtMovePallet(palletNumber, fromLocation, destination, priority, "BK", who.Id);
            return taskNo > 0;
        }

        public bool EmptyLocation(int palletNumber, string location, int priority, Employee who)
        {
            var taskNo = this.wcsPack.EmptyLocation(palletNumber, location, priority, "BK", who.Id);
            return taskNo > 0;
        }

        public IEnumerable<WarehouseLocation> GetWarehouseLocationsWithPallets()
        {
            var locations = this.warehouseLocationRepository.FilterBy(p => p.PalletId != null && p.PalletId <= 3000);
            return locations;
        }
    }
}
