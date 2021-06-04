namespace Linn.Stores.Domain.LinnApps.StockMove
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;

    public class MoveStockService : IMoveStockService
    {
        private readonly IStoresPack storesPack;

        private readonly IKardexPack kardexPack;

        private readonly IRepository<RequisitionHeader, int> requisitionRepository;

        private readonly IRepository<StorageLocation, int> storageLocationRepository;

        private readonly IStoresPalletRepository storesPalletRepository;

        public MoveStockService(
            IStoresPack storesPack,
            IKardexPack kardexPack,
            IRepository<RequisitionHeader, int> requisitionRepository,
            IRepository<StorageLocation, int> storageLocationRepository,
            IStoresPalletRepository storesPalletRepository)
        {
            this.storesPack = storesPack;
            this.kardexPack = kardexPack;
            this.requisitionRepository = requisitionRepository;
            this.storageLocationRepository = storageLocationRepository;
            this.storesPalletRepository = storesPalletRepository;
        }

        public RequisitionProcessResult MoveStock(
            int? reqNumber,
            string partNumber,
            decimal quantity,
            string from,
            int? fromLocationId,
            int? fromPalletNumber,
            DateTime? fromStockRotationDate,
            string fromState,
            string fromStockPool,
            string to,
            int? toLocationId,
            int? toPalletNumber,
            DateTime? toStockRotationDate,
            string storageType,
            int userNumber)
        {
            if (quantity <= 0)
            {
                return new RequisitionProcessResult(false, "You must have a valid quantity.");
            }

            if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
            {
                return new RequisitionProcessResult(false, "A from and to location must be specified.");
            }

            if (this.IsKardexLocation(from) && this.IsKardexLocation(to))
            {
                return new RequisitionProcessResult(false, "You cannot move between two Kardex locations.");
            }

            var result = new RequisitionProcessResult();
            if (!reqNumber.HasValue)
            {
                var createResult = this.storesPack.CreateMoveReq(userNumber);
                if (!createResult.Success)
                {
                    throw new CreateReqFailureException("Failed to create move req");
                }

                result.ReqNumber = createResult.ReqNumber;
            }
            else
            {
                result.ReqNumber = reqNumber.Value;
            }

            var req = this.requisitionRepository.FindById(result.ReqNumber);

            if (!this.IsKardexLocation(from) && !fromLocationId.HasValue && !fromPalletNumber.HasValue)
            {
                this.GetLocationDetails(from, out fromLocationId, out fromPalletNumber);
            }

            if (!this.IsKardexLocation(to) && !toLocationId.HasValue && !toPalletNumber.HasValue)
            {
                this.GetLocationDetails(to, out toLocationId, out toPalletNumber);
            }

            this.CheckPallet(fromPalletNumber);
            this.CheckPallet(toPalletNumber);

            var nextLineNumber = req.Lines.Count() + 1;

            var checkFromLocation = this.storesPack.CheckStockAtFromLocation(
                partNumber,
                quantity,
                from,
                fromLocationId,
                fromPalletNumber,
                fromStockRotationDate);

            if (!checkFromLocation.Success)
            {
                throw new MoveInvalidException("The required stock doesn't exist at that from location.");
            }

            ProcessResult moveResult;
            if (this.IsKardexLocation(from))
            {
                moveResult = this.kardexPack.MoveStockFromKardex(
                    result.ReqNumber,
                    nextLineNumber,
                    from,
                    partNumber,
                    quantity,
                    storageType,
                    toLocationId,
                    toPalletNumber);
            }
            else if (this.IsKardexLocation(to))
            {
                if (!fromStockRotationDate.HasValue && !toStockRotationDate.HasValue)
                {
                    moveResult = new ProcessResult
                                     {
                                         Message = "You must provide either a from or to stock rotation date",
                                         Success = false
                                     };
                }
                else
                {
                    moveResult = this.kardexPack.MoveStockToKardex(
                        result.ReqNumber,
                        nextLineNumber,
                        to,
                        partNumber,
                        quantity,
                        fromStockRotationDate,
                        storageType,
                        fromLocationId,
                        fromPalletNumber,
                        toStockRotationDate,
                        null);
                }
            }
            else
            {
                moveResult = this.storesPack.MoveStock(
                    result.ReqNumber,
                    nextLineNumber,
                    partNumber,
                    quantity,
                    fromLocationId,
                    fromPalletNumber,
                    fromStockRotationDate,
                    toLocationId,
                    toPalletNumber,
                    toStockRotationDate,
                    !string.IsNullOrEmpty(fromState) ? fromState : checkFromLocation.State,
                    fromStockPool);
            }

            result.Success = moveResult.Success;
            result.Message = moveResult.Message;
            return result;
        }

        public bool IsKardexLocation(string location)
        {
            var kardexList = new List<string> { "K1", "K2", "K3", "K4", "K5" };
            var kardexFixedLocationsList = new List<string> { "K1", "K2" };

            if (kardexList.Contains(location))
            {
                return true;
            }

            if (location.Length > 2 && kardexList.Select(k => $"{k}-").Contains(location.Substring(0, 3)))
            {
                return true;
            }

            if (location.Length > 4 && kardexFixedLocationsList.Select(k => $"E-{k}-").Contains(location.Substring(0, 5)))
            {
                return true;
            }

            return false;
        }

        public void GetLocationDetails(string location, out int? locationId, out int? palletNumber)
        {
            if (int.TryParse(location, out _))
            {
                palletNumber = int.Parse(location);
                locationId = null;
                return;
            }

            if (location.Substring(0, 1) == "P" && int.TryParse(location.Substring(1), out _))
            {
                palletNumber = int.Parse(location.Substring(1));
                locationId = null;
                return;
            }

            var storageLocation = this.storageLocationRepository.FindBy(l => l.LocationCode == location);
            if (storageLocation != null)
            {
                locationId = storageLocation.LocationId;
                palletNumber = null;
                return;
            }

            throw new TranslateLocationException($"Could not find a valid location id or pallet number for {location}");
        }

        private void CheckPallet(int? palletNumber)
        {
            if (palletNumber.HasValue)
            {
                var pallet = this.storesPalletRepository.FindById(palletNumber.Value);
                if (pallet == null)
                {
                    throw new MoveInvalidException($"Pallet number {palletNumber} does not exist");
                }
            }
        }
    }
}
