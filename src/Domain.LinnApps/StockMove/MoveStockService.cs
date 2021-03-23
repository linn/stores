namespace Linn.Stores.Domain.LinnApps.StockMove
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;

    public class MoveStockService : IMoveStockService
    {
        private readonly IStoresPack storesPack;

        private readonly IRepository<RequisitionHeader, int> requisitionRepository;

        public MoveStockService(IStoresPack storesPack, IRepository<RequisitionHeader, int> requisitionRepository)
        {
            this.storesPack = storesPack;
            this.requisitionRepository = requisitionRepository;
        }

        public RequisitionProcessResult MoveStock(
            int? reqNumber,
            string partNumber,
            int quantity,
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

            if (!fromLocationId.HasValue && !fromPalletNumber.HasValue)
            {
                this.GetLocationDetails(from, out fromLocationId, out fromPalletNumber);
            }

            if (!toLocationId.HasValue && !toPalletNumber.HasValue)
            {
                this.GetLocationDetails(to, out toLocationId, out toPalletNumber);
            }

            if (this.IsKardexLocation(from) || this.IsKardexLocation(to))
            {
                throw new MoveInvalidException("Not yet implemented");
            }

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

            var moveResult = this.storesPack.MoveStock(
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
             null, // !string.IsNullOrEmpty(fromState) ? fromState : checkFromLocation.State,
                fromStockPool);

            result.Success = moveResult.Success;
            result.Message = moveResult.Message;
            return result;
        }

        public bool IsKardexLocation(string location)
        {
            var kardexList = new List<string> { "K1", "K2", "K3", "K4", "K5" };
            if (kardexList.Contains(location))
            {
                return true;
            }

            if (location.Length > 2 && kardexList.Select(k => $"{k}-").Contains(location.Substring(0, 3)))
            {
                return true;
            }

            if (location.Length > 4 && kardexList.Select(k => $"E-{k}-").Contains(location.Substring(0, 5)))
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

            throw new TranslateLocationException($"Could not find a valid location id or pallet number for {location}");
        }
    }
}
